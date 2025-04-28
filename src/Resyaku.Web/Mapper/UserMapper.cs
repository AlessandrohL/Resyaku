using Resyaku.Infrastructure.Authentication.Features.Users.Commands.CreateUser;
using Resyaku.Infrastructure.Authentication.Features.Users.Commands.UpdateUser;
using Resyaku.Web.ViewModels.Users;

namespace Resyaku.Web.Mapper
{
    public static class UserMapper
    {
        public static CreateUserCommand ToCreateUserCommand(this CreateUserViewModel viewModel)
        {
            return new CreateUserCommand(
                viewModel.Firstname,
                viewModel.Lastname,
                viewModel.Phone,
                viewModel.Email,
                viewModel.Username,
                viewModel.Password,
                viewModel.SelectedRoles);
        }

        public static UpdateUserCommand ToUpdateUserCommand(this UpdateUserViewModel viewModel, string userId)
        {
            return new UpdateUserCommand(
                UserId: userId,
                Firstname: viewModel.Firstname,
                Lastname: viewModel.Lastname,
                Phone: viewModel.Phone,
                Email: viewModel.Email,
                Username: viewModel.Username,
                ChangePassword: viewModel.ChangePassword,
                NewPassword: viewModel.NewPassword,
                LockoutEnabled: viewModel.LockoutEnabled,
                Roles: viewModel.SelectedRoles);
        }
    }
}
