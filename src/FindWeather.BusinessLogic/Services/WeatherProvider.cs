using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Refits;
using FindWeather.BusinessLogic.Services.Interfaces;

namespace FindWeather.BusinessLogic.Services;

public class WeatherProvider(IOpenMeteoApi openMeteoApi, IGeoCodingApi geoCodingApi)
    : IWeatherProvider
{
    public async Task<CurrentWeather> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken)
    {
        var location = await GetLocationAsync(city, cancellationToken);

        var result = await openMeteoApi.GetCurrentTemperatureAsync(location.Latitude, location.Longitude, cancellationToken: cancellationToken);

        return result.CurrentWeather;
    }

    public async Task<WeatherResponse> GetWeatherInRangeAsync(string city, string startDate = null!, string endDate = null!, CancellationToken cancellationToken = default)
    {
        var location = await GetLocationAsync(city, cancellationToken);

        return await openMeteoApi.GetForecastAsync(location.Latitude, location.Longitude, startDate: startDate, endDate: endDate, cancellationToken: cancellationToken);
    }

    public async Task<GeoResult> GetLocationAsync(string city, CancellationToken cancellationToken)
    {
        var result = await geoCodingApi.SearchCityAsync(city, cancellationToken);

        return result.Results[0];
    }
}
