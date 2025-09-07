using FindWeather.BusinessLogic.Helpers;
using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FindWeather.ConsoleApp;

public class AppUI(IWeatherProvider weatherProvider)
{
    public async Task RunAsync()
    {
        string city = GetCityName();

        DisplayForecastType();
        await ProcessUserInput(city);
    }

    private async Task ProcessUserInput(string city)
    {
        var input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.NumPad1:
                var weather = weatherProvider.GetCurrentWeatherAsync(city);
                DisplayCurrentWeather(city, weather.Result);
                break;
            case ConsoleKey.NumPad2:
                var numberOfDays = GetNumberOfDays();
                var weathers = await weatherProvider.GetWeatherInRangeAsync(city, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(numberOfDays).ToString("yyyy-MM-dd"));
                DisplayWeatherForecast(city, weathers);
                break;
        }
    }

    private void DisplayWeatherForecast(string city, WeatherResponse weathers)
    {
        Console.WriteLine($"{city} weather forecast");
        for(var i = 0; i < weathers.Daily.Time.Count; i++)
        {
            var weather = weathers.Daily.Temperature2mMax[i];
            Console.WriteLine($"Day {i + 1}: {weather}. {WeatherCommentHelper.GetComment(weather)}");
        }
    }

    private int GetNumberOfDays()
    {
        Console.Write("\nPlease enter number of days: ");

        var days = Console.ReadLine();

        while(!int.TryParse(days, out int res))
        {
            Console.Write("Please enter only numbers: ");

            days = Console.ReadLine();
        }

        return int.Parse(days) - 1;
    }

    private void DisplayForecastType()
    {
        Console.WriteLine("1. Current weather \r\n" +
            "2. Weather forecast \r\n" +
            "0. Close application");
    }

    private void DisplayCurrentWeather(string city, CurrentWeather weather)
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
