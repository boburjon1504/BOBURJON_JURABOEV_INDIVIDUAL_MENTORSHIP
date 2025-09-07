using FindWeather.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindWeather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController(IWeatherProvider weatherProvider) : ControllerBase
    {
        [HttpGet("current-weather/{city}")]
        public async Task<IActionResult> GetCurrentWeather(string city, CancellationToken cancellationToken)
        {
            var weather = await weatherProvider.GetCurrentWeatherAsync(city, cancellationToken);

            return Ok(weather);
        }

        [HttpGet("forecast/{city}")]
        public async Task<IActionResult> GetTemperatureInRange(string city, [FromQuery] int days)
        {
            var weathers = await weatherProvider
                                .GetWeatherInRangeAsync(city,
                                DateTime.Now.ToString("yyyy-MM-dd"),
                                DateTime.Now.AddDays(days - 1).ToString("yyyy-MM-dd"));

            return Ok(weathers);
        }
    }
}
