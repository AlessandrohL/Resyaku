using Resyaku.Domain.Primitives;

namespace Resyaku.Application.Errors
{
    public static class TableErrors
    {
        public static readonly Error NotFound = new(
            "Table.NotFound",
            ErrorType.Problem,
            "Mesa no encontrada.");

    }
}
