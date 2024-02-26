using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WeatherService;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet("/forecast")]
    public async Task<IActionResult> Get()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return await Task.FromResult(Ok(forecast));
    }
}