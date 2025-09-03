using FindWeather.BusinessLogic.Helpers;
using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Services.Interfaces;

namespace FindWeather.ConsoleApp;

public class AppUI(IWeatherProvider weatherProvider)
{
    public async Task RunAsync()
    {
        string city = GetCityName();

        var weather = await weatherProvider.GetCurrentWeatherAsync(city);

        Display(city, weather);
    }

    private void Display(string city, CurrentWeather weather)
    {
        Console.WriteLine($"In {city} {weather.Temperature} °C. {WeatherCommentHelper.GetComment(weather.Temperature)}");
    }

    private static string GetCityName()
    {
        Console.Write("Enter city name: ");

        var city = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(city))
        {
            Console.Write("City name is required. Please enter city name: ");
            city = Console.ReadLine();
        }

        return city;
    }
}
