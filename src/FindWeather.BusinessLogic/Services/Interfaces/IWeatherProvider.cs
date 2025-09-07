using FindWeather.BusinessLogic.Models;

namespace FindWeather.BusinessLogic.Services.Interfaces;

public interface IWeatherProvider
{
    Task<WeatherResponse> GetWeatherInRangeAsync(string city, string startDate = default!, string endDate = default!, CancellationToken cancellationToken = default);

    Task<CurrentWeather> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken);
}
