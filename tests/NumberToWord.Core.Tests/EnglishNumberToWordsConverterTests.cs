using NumberToWord.Core.Converters;

namespace NumberToWord.Core.Tests;

public sealed class EnglishNumberToWordsConverterTests
{
    private readonly EnglishNumberToWordsConverter _converter = new();

    [Theory]
    [InlineData(0, "zero")]
    [InlineData(1, "one")]
    [InlineData(10, "ten")]
    [InlineData(11, "eleven")]
    [InlineData(19, "nineteen")]
    [InlineData(20, "twenty")]
    [InlineData(21, "twenty-one")]
    [InlineData(45, "forty-five")]
    [InlineData(99, "ninety-nine")]
    [InlineData(100, "one hundred")]
    [InlineData(101, "one hundred one")]
    [InlineData(125, "one hundred twenty-five")]
    [InlineData(999, "nine hundred ninety-nine")]
    [InlineData(1_000, "one thousand")]
    [InlineData(1_001, "one thousand one")]
    [InlineData(1_121, "one thousand one hundred twenty-one")]
    [InlineData(21_000, "twenty-one thousand")]
    [InlineData(999_999, "nine hundred ninety-nine thousand nine hundred ninety-nine")]
    [InlineData(1_000_000, "one million")]
    [InlineData(1_375_123, "one million three hundred seventy-five thousand one hundred twenty-three")]
    [InlineData(1_000_001, "one million one")]
    [InlineData(
        999_999_999,
        "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine")]
    public void Convert_ReturnsExpectedWords(long number, string expected)
    {
        var result = _converter.Convert(number);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000_000_000)]
    public void Convert_WhenNumberIsOutsideRange_Throws(long number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _converter.Convert(number));
    }
}