using Resyaku.Domain.Primitives;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetAllUsers;

namespace Resyaku.Web.ViewModels.Users
{
    public class GetAllUsersViewModel(
        GetAllUsersQueryParameters queryParameters,
        PagedList<GetAllUsersDto> userResponses)
    {
        public GetAllUsersQueryParameters QueryParameters { get; init; } = queryParameters;
        public PagedList<GetAllUsersDto> UserResponses { get; init; } = userResponses;
    }
}
