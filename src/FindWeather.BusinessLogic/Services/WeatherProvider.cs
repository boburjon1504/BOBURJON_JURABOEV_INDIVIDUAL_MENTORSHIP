using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Refits;
using FindWeather.BusinessLogic.Services.Interfaces;

namespace FindWeather.BusinessLogic.Services;

public class WeatherProvider(IOpenMeteoApi openMeteoApi, IGeoCodingApi geoCodingApi)
    : IWeatherProvider
{
    public async Task<CurrentWeather> GetCurrentWeatherAsync(string city)
    {
        var location = await GetLocationAsync(city);

        var result = await openMeteoApi.GetCurrentTemperatureAsync(location.Latitude, location.Longitude);

        return result.CurrentWeather;
    }

    public async Task<WeatherResponse> GetWeatherInRangeAsync(string city, string startDate = null!, string endDate = null!)
    {
        var location = await GetLocationAsync(city);

        return await openMeteoApi.GetForecastAsync(location.Latitude, location.Longitude, startDate: startDate, endDate: endDate);
    }

    public async Task<GeoResult> GetLocationAsync(string city)
    {
        var result = await geoCodingApi.SearchCityAsync(city);

        return result.Results[0];
    }
}
