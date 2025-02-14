namespace Resyaku.Domain.Primitives
{
    public sealed record Error(string Code, ErrorType Type, string Description)
    {
        public static readonly Error None = new(string.Empty, ErrorType.None, string.Empty);
    }

    public enum ErrorType
    {
        None,
        Problem,
        Validation,
        NotFound,
        Conflict
    }
}
