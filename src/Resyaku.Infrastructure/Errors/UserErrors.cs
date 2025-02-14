using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Errors
{
    public static class UserErrors
    {
        public static readonly Error UserAlreadyExists = new(
          "User.AlreadyExists",
          ErrorType.Problem,
          "El correo electrónico proporcionado ya está en uso.");

        public static readonly Error NotFound = new(
            "User.NotFound",
            ErrorType.NotFound,
            "Usuario no encontrado.");

        public static readonly Error EmailAlreadyConfirmed = new(
            "User.AlreadyConfirmed",
            ErrorType.Conflict,
            "Este usuario ya ha sido confirmado.");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            ErrorType.Problem,
            "Correo electrónico o contraseña no válidos. Por favor verifique sus credenciales.");

        public static readonly Error UnconfirmedEmail = new(
            "User.UnconfirmedEmail",
            ErrorType.Problem,
            "El correo electrónico del usuario aún no ha sido confirmado.");

        public static readonly Error AlreadyInRole = new(
            "User.AlreadyInRole",
            ErrorType.Conflict,
            "El usuario ya pertenece a uno de los roles especificados.");

        public static readonly Error PasswordNotUpdated = new(
            "User.PasswordNotUpdated",
            ErrorType.Problem,
            "No se pudo actualizar la contraseña");

    }
}
