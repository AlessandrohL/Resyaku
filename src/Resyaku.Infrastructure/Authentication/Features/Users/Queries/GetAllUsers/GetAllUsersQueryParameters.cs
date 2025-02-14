using System.Linq.Expressions;
using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Identity;

namespace Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers
{
    public sealed class GetAllUsersQueryParameters : PaginationParameters, IQueryFilter<ApplicationUser>
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }

        public Expression<Func<ApplicationUser, object>> GetSortProperty()
        {
            return SortColumn?.ToLower() switch
            {
                "firstname" => applicationUser => applicationUser.Firstname,
                "email" => applicationUser => applicationUser.Email!,
                _ => applicationUser => applicationUser.Id
            };
        }
    }
}
