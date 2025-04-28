using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers;
using Resyaku.Infrastructure.DTOs.Users;

namespace Resyaku.Web.ViewModels.Users
{
    public class GetAllUsersViewModel(
        GetAllUsersQueryParameters queryParameters,
        PagedList<UserInfoDto> userResponses)
    {
        public GetAllUsersQueryParameters QueryParameters { get; init; } = queryParameters;
        public PagedList<UserInfoDto> UserResponses { get; init; } = userResponses;
    }
}
