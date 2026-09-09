using NumberToWord.Core.Factories;
using NumberToWord.Core.Models;
using NumberToWord.Core.Services;

namespace NumberToWord.Core.Tests;

public sealed class CurrencyConversionServiceTests
{
    private readonly ICurrencyConversionService _service;

    public CurrencyConversionServiceTests()
    {
        var factory = new NumberToWordsConverterFactory();

        _service = new CurrencyConversionService(factory);
    }

    [Theory]
    [InlineData(
        0,
        0,
        "zero dollars")]

    [InlineData(
        1,
        0,
        "one dollar")]

    [InlineData(
        25,
        10,
        "twenty-five dollars and ten cents")]

    [InlineData(
        0,
        1,
        "zero dollars and one cent")]

    [InlineData(
        45_100,
        0,
        "forty-five thousand one hundred dollars")]

    [InlineData(
        999_999_999,
        99,
        "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
    public void Convert_English_ReturnsExpectedResult(
        int dollars,
        int cents,
        string expected)
    {
        var amount = new CurrencyAmount(dollars, cents);

        var result = _service.Convert(amount, "en");

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(
        0,
        0,
        "null Dollar")]

    [InlineData(
        1,
        0,
        "ein Dollar")]

    [InlineData(
        25,
        10,
        "fünfundzwanzig Dollar und zehn Cent")]

    [InlineData(
        0,
        1,
        "null Dollar und ein Cent")]

    [InlineData(
        45_100,
        0,
        "fünfundvierzigtausendeinhundert Dollar")]

    public void Convert_German_ReturnsExpectedResult(
        int dollars,
        int cents,
        string expected)
    {
        var amount = new CurrencyAmount(dollars, cents);

        var result = _service.Convert(amount, "de");

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_UnsupportedLanguage_Throws()
    {
        var amount = new CurrencyAmount(100, 50);

        Assert.Throws<ArgumentException>(
            () => _service.Convert(amount, "fr"));
    }
}