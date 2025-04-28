namespace Resyaku.Domain.Abstractions
{
    public interface IBookingReferenceProvider
    {
        string Create(int length = 7);
    }
}
