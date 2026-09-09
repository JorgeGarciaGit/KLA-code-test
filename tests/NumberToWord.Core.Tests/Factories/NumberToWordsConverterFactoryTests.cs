using NumberToWord.Core.Converters;
using NumberToWord.Core.Factories;

namespace NumberToWord.Core.Tests;

public sealed class NumberToWordsConverterFactoryTests
{
    private readonly INumberToWordsConverterFactory _factory = new NumberToWordsConverterFactory();

    [Fact]
    public void Get_English_ReturnsEnglishConverter()
    {
        var converter = _factory.Get("en");

        Assert.IsType<EnglishNumberToWordsConverter>(converter);
    }

    [Fact]
    public void Get_German_ReturnsGermanConverter()
    {
        var converter = _factory.Get("de");

        Assert.IsType<GermanNumberToWordsConverter>(converter);
    }

    [Theory]
    [InlineData("EN")]
    [InlineData("En")]
    [InlineData("eN")]
    public void Get_English_IsCaseInsensitive(string language)
    {
        var converter = _factory.Get(language);

        Assert.IsType<EnglishNumberToWordsConverter>(converter);
    }

    [Theory]
    [InlineData("DE")]
    [InlineData("De")]
    [InlineData("dE")]
    public void Get_German_IsCaseInsensitive(string language)
    {
        var converter = _factory.Get(language);

        Assert.IsType<GermanNumberToWordsConverter>(converter);
    }

    [Theory]
    [InlineData("fr")]
    [InlineData("")]
    [InlineData("es")]
    public void Get_UnsupportedLanguage_Throws(string language)
    {
        Assert.Throws<ArgumentException>(
            () => _factory.Get(language));
    }
}