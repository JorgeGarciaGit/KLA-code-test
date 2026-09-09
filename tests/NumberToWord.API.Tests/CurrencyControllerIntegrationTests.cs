using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NumberToWord.API.Contracts;
namespace NumberToWord.API.Tests;

public sealed class CurrencyControllerIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CurrencyControllerIntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory
        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        })
        .CreateClient();
    }

    [Fact]
    public async Task Convert_English_ReturnsExpectedResult()
    {
        var request = new ConvertCurrencyRequest(
            45_100,
            10,
            "en");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<ConvertCurrencyResponse>();

        Assert.NotNull(result);
        Assert.Equal(
            "forty-five thousand one hundred dollars and ten cents",
            result.Text);
    }

    [Fact]
    public async Task Convert_German_ReturnsExpectedResult()
    {
        var request = new ConvertCurrencyRequest(
            45_100,
            10,
            "de");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<ConvertCurrencyResponse>();

        Assert.NotNull(result);
        Assert.Equal(
            "fünfundvierzigtausendeinhundert Dollar und zehn Cent",
            result.Text);
    }

    [Fact]
    public async Task Convert_InvalidDollars_ReturnsBadRequest()
    {
        var request = new ConvertCurrencyRequest(
            -1,
            50,
            "en");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task Convert_DollarsAboveMaximum_ReturnsBadRequest()
    {
        var request = new ConvertCurrencyRequest(
            1_000_000_000,
            50,
            "en");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task Convert_InvalidCents_ReturnsBadRequest()
    {
        var request = new ConvertCurrencyRequest(
            100,
            100,
            "en");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task Convert_UnsupportedLanguage_ReturnsBadRequest()
    {
        var request = new ConvertCurrencyRequest(
            100,
            50,
            "fr");

        var response = await _client.PostAsJsonAsync(
            "/api/currency/convert",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }
}