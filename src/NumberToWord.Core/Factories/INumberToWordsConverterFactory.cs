using NumberToWord.Core.Converters;

namespace NumberToWord.Core.Factories;

public interface INumberToWordsConverterFactory
{
    INumberToWordsConverter Get(string language);
}