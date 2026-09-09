
using NumberToWord.API.Middleware;
using NumberToWord.Core.Factories;
using NumberToWord.Core.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactClient", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});




builder.Services.AddSingleton<
    INumberToWordsConverterFactory,
    NumberToWordsConverterFactory>();

builder.Services.AddScoped<
    ICurrencyConversionService,
    CurrencyConversionService>();


var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("ReactClient");

app.MapControllers();
app.Run();

public partial class Program
{
}