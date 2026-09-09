namespace NumberToWord.Core.Converters;

public sealed class EnglishNumberToWordsConverter : INumberToWordsConverter
{
    private static readonly string[] Units =
    [
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
        "ten",
        "eleven",
        "twelve",
        "thirteen",
        "fourteen",
        "fifteen",
        "sixteen",
        "seventeen",
        "eighteen",
        "nineteen"
    ];

    private static readonly string[] Tens =
    [
        "",
        "",
        "twenty",
        "thirty",
        "forty",
        "fifty",
        "sixty",
        "seventy",
        "eighty",
        "ninety"
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

        return units == 0
            ? Tens[tens]
            : $"{Tens[tens]}-{Units[units]}";
    }

    private string ConvertHundreds(long number)
    {
        var hundreds = number / 100;
        long remainder = number % 100;

        if (remainder == 0)
            return $"{Units[hundreds]} hundred";

        return $"{Units[hundreds]} hundred {Convert(remainder)}";
    }

    private string ConvertThousands(long number)
    {
        long thousands = number / 1_000;
        long remainder = number % 1_000;

        var result = $"{Convert(thousands)} thousand";

        if (remainder > 0)
            result += $" {Convert(remainder)}";

        return result;
    }

    private string ConvertMillions(long number)
    {
        long millions = number / 1_000_000;
        long remainder = number % 1_000_000;

        var result = $"{Convert(millions)} million";

        if (remainder > 0)
            result += $" {Convert(remainder)}";

        return result;
    }
}