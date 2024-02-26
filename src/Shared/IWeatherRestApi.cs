using Microsoft.AspNetCore.Mvc;
using Refit;
using Shared.Models;

namespace Shared;

public interface IWeatherRestApi
{
    [Get("/forecast")]
    public Task<WeatherForecast[]> Weather();
}