using NumberToWord.Core.Converters;

namespace NumberToWord.Core.Factories;

public sealed class NumberToWordsConverterFactory
    : INumberToWordsConverterFactory
{
    public INumberToWordsConverter Get(string language)
    {
        return language.ToLowerInvariant() switch
        {
            "en" => new EnglishNumberToWordsConverter(),
            "de" => new GermanNumberToWordsConverter(),
            _ => throw new ArgumentException(
                $"Unsupported language: {language}",
                nameof(language))
        };
    }
}