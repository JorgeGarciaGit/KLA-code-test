using NumberToWord.Core.Factories;
using NumberToWord.Core.Models;

namespace NumberToWord.Core.Services;

public sealed class CurrencyConversionService : ICurrencyConversionService
{
    private readonly INumberToWordsConverterFactory _converterFactory;

    public CurrencyConversionService(
        INumberToWordsConverterFactory converterFactory)
    {
        _converterFactory = converterFactory;
    }

    public string Convert(CurrencyAmount amount, string language)
    {
        var converter = _converterFactory.Get(language);

        var dollars = converter.Convert(amount.Dollars);

        if (language.Equals("de", StringComparison.OrdinalIgnoreCase) && amount.Dollars == 1)
        {
            dollars = "ein";
        }

        if (amount.Cents == 0)
            return BuildDollarResult(dollars, amount, language);

        var cents = converter.Convert(amount.Cents);

        if (language.Equals("de", StringComparison.OrdinalIgnoreCase) && amount.Cents == 1)
        {
            cents = "ein";
        }
        return BuildCurrencyResult(
            dollars,
            cents,
            amount,
            language);
    }

    private static string BuildDollarResult(
        string dollars,
        CurrencyAmount amount,
        string language)
    {
        return language.ToLowerInvariant() switch
        {
            "en" => $"{dollars} {(amount.Dollars == 1 ? "dollar" : "dollars")}",

            "de" => $"{dollars} Dollar",

            _ => throw new ArgumentException(
                $"Unsupported language: {language}",
                nameof(language))
        };
    }

    private static string BuildCurrencyResult(
        string dollars,
        string cents,
        CurrencyAmount amount,
        string language)
    {
        return language.ToLowerInvariant() switch
        {
            "en" =>
                $"{dollars} {(amount.Dollars == 1 ? "dollar" : "dollars")} " +
                $"and {cents} {(amount.Cents == 1 ? "cent" : "cents")}",

            "de" =>
                $"{dollars} Dollar und {cents} {(amount.Cents == 1 ? "Cent" : "Cent")}",

            _ => throw new ArgumentException(
                $"Unsupported language: {language}",
                nameof(language))
        };
    }
}