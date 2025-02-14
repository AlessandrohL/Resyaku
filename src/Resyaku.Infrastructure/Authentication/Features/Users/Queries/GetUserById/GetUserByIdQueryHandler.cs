using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.Errors;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager)
        : IRequestHandler<GetUserByIdQuery, Result<GetUserByIdDto>>
    {
        public async Task<Result<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager
                .Users
                .AsNoTracking()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Id == Guid.Parse(request.UserId))
                .Select(u => new GetUserByIdDto
                {
                    UserId = u.Id.ToString(),
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Fullname = u.GetFullname(),
                    Username = u.UserName!,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!,
                    LockoutEnabled = u.LockoutEnabled,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name!)
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return Result.Failure<GetUserByIdDto>(UserErrors.NotFound);
            }

            return Result.Success(user);
        }
    }
}
