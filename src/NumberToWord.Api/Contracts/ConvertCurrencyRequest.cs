namespace NumberToWord.API.Contracts;

public sealed record ConvertCurrencyRequest(int Dollars, int Cents, string Language);