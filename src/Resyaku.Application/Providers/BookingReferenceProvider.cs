using Resyaku.Domain.Abstractions;

namespace Resyaku.Application.Providers;

public sealed class BookingReferenceProvider : IBookingReferenceProvider
{
    private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly Random Random = new();

    public string Create(int length = 7)
    {
        return new string(Enumerable.Repeat(Characters, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}
