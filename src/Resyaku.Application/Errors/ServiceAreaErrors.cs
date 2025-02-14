using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class ServiceAreaErrors
    {
        public static readonly Error NotFound = new(
            "ServiceArea.NotFound",
            ErrorType.Problem,
            "No se encontró el área de servicio específicado.");
    }
}
