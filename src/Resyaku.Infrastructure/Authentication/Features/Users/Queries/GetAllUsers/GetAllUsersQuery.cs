using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;
using Resyaku.Infrastructure.DTOs.Users;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers
{
    public sealed record GetAllUsersQuery(GetAllUsersQueryParameters Parameters)
        : IRequest<PagedList<UserInfoDto>>;

    public sealed class GetAllUsersQueryHandler(
        UserManager<ApplicationUser> userManager)
        : IRequestHandler<GetAllUsersQuery, PagedList<UserInfoDto>>
    {
        public async Task<PagedList<UserInfoDto>> Handle(
            GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var parameters = request.Parameters;
            var query = userManager
                .Users
                .AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(parameters.Firstname),
                    p => EF.Functions.Like(p.Firstname, $"%{parameters.Firstname}%"))
                .WhereIf(!string.IsNullOrEmpty(parameters.Lastname),
                    p => EF.Functions.Like(p.Lastname, $"%{parameters.Lastname}%"))
                .WhereIf(!string.IsNullOrEmpty(parameters.Email),
                    p => EF.Functions.Like(p.Email, $"%{parameters.Email}%"));

            int totalCount = await query.CountAsync(cancellationToken);
            var users = await query
                .ApplyOrdering(parameters)
                .ApplyPagination(parameters)
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
                .ToListAsync(cancellationToken);

            return new PagedList<UserInfoDto>(
                items: users,
                page: parameters.Page,
                pageSize: parameters.PageSize,
                totalCount: totalCount);
        }
    }
}
