using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EcoFarm.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Lấy thông tin thời tiết ngẫu nhiên
    /// </summary>
    /// <param name="date">Ngày</param>
    /// <param name="password">Mật khẩu</param>
    /// <returns></returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromQuery] DateOnly? date, [FromQuery, DataType(DataType.Password)] string password)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = date.HasValue ? date.Value.AddDays(index) : DateOnly.MinValue,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}