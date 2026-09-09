
namespace NumberToWord.Core.Models;

public sealed record CurrencyAmount
{
    public int Dollars { get; }
    public int Cents { get; }

    public CurrencyAmount(int dollars, int cents)
    {
        if (dollars < 0 || dollars > 999_999_999)
            throw new ArgumentOutOfRangeException(nameof(dollars));

        if (cents < 0 || cents > 99)
            throw new ArgumentOutOfRangeException(nameof(cents));

        Dollars = dollars;
        Cents = cents;
    }
}