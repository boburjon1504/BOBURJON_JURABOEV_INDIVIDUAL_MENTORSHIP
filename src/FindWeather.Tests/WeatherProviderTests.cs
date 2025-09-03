using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Refits;
using FindWeather.BusinessLogic.Services;
using Moq;

namespace FindWeather.Tests;

public class WeatherProviderTests
{
    [Fact]
    public async Task GetCurrentWeatherAsyncShouldReturnCorrectWeather()
    {
        var city = "Tashkent";
        var latitude = 41.2995;
        var longitude = 69.2401;

        var openMeteoApiMock = new Mock<IOpenMeteoApi>();
        var geoCodingApiMock = new Mock<IGeoCodingApi>();

        geoCodingApiMock
            .Setup(g => g.SearchCityAsync(city))
            .ReturnsAsync(new GeoCodingResponse
            {
                Results = [ new GeoResult { Latitude = latitude, Longitude = longitude }]
            });

        openMeteoApiMock.Setup(o => o.GetCurrentTemperatureAsync(latitude, longitude, true)).ReturnsAsync(new CurrentWeatherReponse
        {
            CurrentWeather = new CurrentWeather
            {
                Temperature = 25
            }
        });

        var weatherProvider = new WeatherProvider(openMeteoApiMock.Object, geoCodingApiMock.Object);

        var currentWeather = await weatherProvider.GetCurrentWeatherAsync(city);

        Assert.Equal(25, currentWeather.Temperature);
    }
}
