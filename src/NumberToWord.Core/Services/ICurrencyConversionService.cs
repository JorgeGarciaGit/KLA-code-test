using NumberToWord.Core.Models;

namespace NumberToWord.Core.Services;

public interface ICurrencyConversionService
{
    string Convert(CurrencyAmount amount, string language);
}