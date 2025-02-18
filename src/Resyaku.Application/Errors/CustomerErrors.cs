using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class CustomerErrors
    {
        public static readonly Error NotFound = new(
            "Customer.NotFound",
            ErrorType.Problem,
            "No se encontró al cliente.");

        public static readonly Error EmailAlreadyInUse = new(
            "Customer.EmailAlreadyInUse",
            ErrorType.Conflict,
            "El correo electrónico ya está en uso.");
    }
}
