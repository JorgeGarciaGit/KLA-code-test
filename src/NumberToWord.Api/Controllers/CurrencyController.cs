using Microsoft.AspNetCore.Mvc;
using NumberToWord.Core.Services;
using NumberToWord.API.Contracts;
using NumberToWord.Core.Models;
namespace NumberToWord.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CurrencyController : ControllerBase
{
    private readonly ICurrencyConversionService _currencyConversionService;

    public CurrencyController(
        ICurrencyConversionService currencyConversionService)
    {
        _currencyConversionService = currencyConversionService;
    }

    [HttpPost("convert")]
    public ActionResult<ConvertCurrencyResponse> Convert(
        ConvertCurrencyRequest request)
    {
        var amount = new CurrencyAmount(
            request.Dollars,
            request.Cents);

        var result = _currencyConversionService.Convert(
            amount,
            request.Language);

        return Ok(new ConvertCurrencyResponse(result));
    }
}