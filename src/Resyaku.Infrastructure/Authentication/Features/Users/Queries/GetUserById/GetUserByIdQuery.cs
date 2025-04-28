using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.DTOs.Users;
using Resyaku.Infrastructure.Errors;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string UserId) : IRequest<Result<UserInfoDto>>;

    public sealed class GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<GetUserByIdQuery, Result<UserInfoDto>>
    {
        public async Task<Result<UserInfoDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager
                .Users
                .AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Id == Guid.Parse(request.UserId))
                .Select(u => new UserInfoDto(
                    u.Id,
                    u.Firstname,
                    u.Lastname,
                    $"{u.Firstname} {u.Lastname}",
                    u.UserName!,
                    u.Email!,
                    u.PhoneNumber!,
                    u.LockoutEnabled,
                    u.UserRoles.Select(ur => ur.Role.Name)!))
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserInfoDto>(UserErrors.NotFound);
            }

            return Result.Success(user);
        }
    }
}
