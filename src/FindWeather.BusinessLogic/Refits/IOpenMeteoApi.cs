using FindWeather.BusinessLogic.Models;
using Refit;

namespace FindWeather.BusinessLogic.Refits;

public interface IOpenMeteoApi
{
    [Get("/v1/forecast")]
    Task<WeatherResponse> GetForecastAsync(
        [AliasAs("latitude")] double latitude,
        [AliasAs("longitude")] double longitude,
        [AliasAs("daily")] string daily = "temperature_2m_max,temperature_2m_min",
        [AliasAs("start_date")] string startDate = default!,
        [AliasAs("end_date")] string endDate = default!,
        [AliasAs("timezone")] string timezone = "auto", CancellationToken cancellationToken = default);

    [Get("/v1/forecast")]
    Task<CurrentWeatherReponse> GetCurrentTemperatureAsync(
        [AliasAs("latitude")] double latitude,
        [AliasAs("longitude")] double longitude,
        [AliasAs("current_weather")] bool currentWeather = true, CancellationToken cancellationToken = default);
}
