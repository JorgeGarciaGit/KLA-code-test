namespace NumberToWord.Core.Converters;

public sealed class GermanNumberToWordsConverter : INumberToWordsConverter
{
    private static readonly string[] Units =
    [
        "null",
        "eins",
        "zwei",
        "drei",
        "vier",
        "fünf",
        "sechs",
        "sieben",
        "acht",
        "neun",
        "zehn",
        "elf",
        "zwölf",
        "dreizehn",
        "vierzehn",
        "fünfzehn",
        "sechzehn",
        "siebzehn",
        "achtzehn",
        "neunzehn"
    ];

    private static readonly string[] Tens =
    [
        "",
        "",
        "zwanzig",
        "dreizig",
        "vierzig",
        "fünfzig",
        "sechzig",
        "siebzig",
        "achtzig",
        "neunzig"
    ];

    public string Convert(long number)
    {
        if (number < 0 || number > 999_999_999)
            throw new ArgumentOutOfRangeException(nameof(number));

        if (number < 20)
            return Units[number];

        if (number < 100)
            return ConvertTens(number);

        if (number < 1_000)
            return ConvertHundreds(number);

        if (number < 1_000_000)
            return ConvertThousands(number);

        return ConvertMillions(number);
    }

    private string ConvertTens(long number)
    {
        var tens = number / 10;
        var units = number % 10;

        if (units == 0)
            return Tens[tens];

        var unitWord = units == 1
            ? "ein"
            : Units[units];

        return $"{unitWord}und{Tens[tens]}";
    }

    private string ConvertHundreds(long number)
    {
        var hundreds = number / 100;
        var remainder = number % 100;

        var result = hundreds == 1
            ? "einhundert"
            : $"{Units[hundreds]}hundert";

        if (remainder > 0)
            result += Convert(remainder);

        return result;
    }

    private string ConvertThousands(long number)
    {
        var thousands = number / 1_000;
        var remainder = number % 1_000;

        var result = thousands == 1
            ? "eintausend"
            : $"{Convert(thousands)}tausend";

        if (remainder > 0)
            result += Convert(remainder);

        return result;
    }

    private string ConvertMillions(long number)
    {
        var millions = number / 1_000_000;
        var remainder = number % 1_000_000;

        var result = millions == 1
            ? "eine Million"
            : $"{Convert(millions)} Millionen";

        if (remainder > 0)
            result += $" {Convert(remainder)}";

        return result;
    }
}