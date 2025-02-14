using Resyaku.Infrastructure.Authentication.Features.Users.Commands.UpdateUser;
using Resyaku.Infrastructure.Authentication.Features.Users.Queries.GetUserById;
using Resyaku.Web.ViewModels.Users;

namespace Resyaku.Web.Mappers
{
    public static class UserMappers
    {
        public static UpdateUserViewModel ToUpdateUserViewModel(this GetUserByIdDto userDto)
        {
            return new UpdateUserViewModel(
                userId: userDto.UserId,
                firstname: userDto.Firstname,
                lastname: userDto.Lastname,
                phone: userDto.PhoneNumber,
                email: userDto.Email,
                username: userDto.Username,
                changePassword: false,
                newPassword: string.Empty,
                lockoutEnabled: userDto.LockoutEnabled,
                roles: userDto.Roles ?? []
            );
        }

        public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserViewModel viewModel)
        {
            return new UpdateUserCommand(
                UserId: viewModel.UserId,
                Firstname: viewModel.Firstname,
                Lastname: viewModel.Lastname,
                Phone: viewModel.Phone,
                Email: viewModel.Email,
                Username: viewModel.Username,
                ChangePassword: viewModel.ChangePassword,
                NewPassword: viewModel.NewPassword,
                LockoutEnabled: viewModel.LockoutEnabled,
                Roles: viewModel.Roles);
        }
    }
}
