using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resyaku.Domain.Extensions;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers
{
    public sealed class GetAllUsersQueryHandler(
        UserManager<ApplicationUser> userManager)
        : IRequestHandler<GetAllUsersQuery, PagedList<GetAllUsersDto>>
    {
        public async Task<PagedList<GetAllUsersDto>> Handle(
            GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var parameters = request.Parameters;

            var query = userManager
                .Users
                .AsNoTracking()
                .WhereIf(!string.IsNullOrEmpty(parameters.Firstname), p => EF.Functions.Like(p.Firstname, $"%{parameters.Firstname}%"))
                .WhereIf(!string.IsNullOrEmpty(parameters.Lastname), p => EF.Functions.Like(p.Lastname, $"%{parameters.Lastname}%"))
                .WhereIf(!string.IsNullOrEmpty(parameters.Email), p => EF.Functions.Like(p.Email, $"%{parameters.Email}%"));

            int totalCount = await query.CountAsync(CancellationToken.None);

            List<GetAllUsersDto> users = await query
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ApplyOrdering(parameters)
                .ApplyPagination(parameters)
                .Select(u => new GetAllUsersDto
                {
                    UserId = u.Id.ToString(),
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Fullname = $"{u.Firstname} {u.Lastname ?? string.Empty}",
                    Username = u.UserName!,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!,
                    LockoutEnabled = u.LockoutEnabled,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name!)
                })
                .ToListAsync(CancellationToken.None);

            return new PagedList<GetAllUsersDto>(
                items: users,
                page: parameters.Page,
                pageSize: parameters.PageSize,
                totalCount: totalCount);
        }
    }
}
