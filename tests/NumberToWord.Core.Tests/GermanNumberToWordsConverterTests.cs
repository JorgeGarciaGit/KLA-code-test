using NumberToWord.Core.Converters;

namespace NumberToWord.Core.Tests;

public sealed class GermanNumberToWordsConverterTests
{
    private readonly GermanNumberToWordsConverter _converter = new();

    [Theory]
    [InlineData(0, "null")]
    [InlineData(1, "eins")]
    [InlineData(10, "zehn")]
    [InlineData(11, "elf")]
    [InlineData(19, "neunzehn")]
    [InlineData(20, "zwanzig")]
    [InlineData(21, "einundzwanzig")]
    [InlineData(45, "fünfundvierzig")]
    [InlineData(99, "neunundneunzig")]
    [InlineData(100, "einhundert")]
    [InlineData(101, "einhunderteins")]
    [InlineData(125, "einhundertfünfundzwanzig")]
    [InlineData(999, "neunhundertneunundneunzig")]
    [InlineData(1_000, "eintausend")]
    [InlineData(1_001, "eintausendeins")]
    [InlineData(1_121, "eintausendeinhunderteinundzwanzig")]
    [InlineData(21_000, "einundzwanzigtausend")]
    [InlineData(999_999, "neunhundertneunundneunzigtausendneunhundertneunundneunzig")]
    [InlineData(1_000_000, "eine Million")]
    [InlineData(1_000_001, "eine Million eins")]
    [InlineData(1_375_123, "eine Million dreihundertfünfundsiebzigtausendeinhundertdreiundzwanzig")]
    [InlineData(2_000_000, "zwei Millionen")]
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