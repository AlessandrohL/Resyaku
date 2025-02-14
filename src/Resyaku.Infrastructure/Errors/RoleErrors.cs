using Resyaku.Domain.Primitives;

namespace Resyaku.Infrastructure.Errors
{
    public static class RoleErrors
    {
        public static readonly Error NotFound = new(
          "Role.NotFound",
          ErrorType.Problem,
          "El role especificado no existe.");
    }
}
