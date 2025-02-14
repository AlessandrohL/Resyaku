using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class CustomerErrors
    {
        public static readonly Error NotFound = new(
            "Customer.NotFound",
            ErrorType.Problem,
            "No se encontró al cliente.");
    }
}
