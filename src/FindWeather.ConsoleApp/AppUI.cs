using FindWeather.BusinessLogic.Helpers;
using FindWeather.BusinessLogic.Models;
using FindWeather.BusinessLogic.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FindWeather.ConsoleApp;

public class AppUI(IWeatherProvider weatherProvider)
{
    public async Task RunAsync()
    {
        Console.WriteLine("Choose how many city you want to search:\n" +
            "1. Only 1.\n2. More than one.");

        var input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.NumPad1:
                string city = GetCityName();

                DisplayForecastType();

                await ProcessUserInput(city);

                break;
            case ConsoleKey.NumPad2:
                Console.WriteLine("Enter city names. Every time you write pres enter to write new city name: ");

                List<string> cities = GetUserInputCities();

                var tasks = cities.Select(async city =>
                {
                    var stopWatch = Stopwatch.StartNew();
                    try
                    {
                        var weather = await weatherProvider.GetCurrentWeatherAsync(city);
                        Console.WriteLine($"SUCCESS CASE - City: {city} - {weather.Temperature}°C.  Timer - {stopWatch.ElapsedMilliseconds}ms");
                        return (city, weather.Temperature);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"ON FAIL - City: {city}. Error {ex.Message}. Timer - {stopWatch.ElapsedMilliseconds}");
                    }
                    finally
                    {
                        stopWatch.Stop();
                    }

                    return ("no city", -1000);
                });

                var temperatures = await Task.WhenAll(tasks);

                var max = temperatures.MaxBy(t => t.Temperature);
                var success = temperatures.Where(t => t.Temperature != -1000).Count();
                var errors = temperatures.Length - success;

                if(success == 0)
                {
                    Console.WriteLine("Error no successfull request");
                }
                else
                {
                    Console.WriteLine($"City with max temperature {max.Temperature}°C is {max.city}\n" +
                        $"Successfull requests count: {success}\n" +
                        $"Failed request count: {errors}");
                }

                break;
            default: break;
        }

    }

    private static List<string> GetUserInputCities()
    {
        var cities = new List<string>();

        var cityName = Console.ReadLine();

        while (cityName?.Length > 0)
        {
            cities.Add(cityName);

            Console.Write("Enter new city name: ");

            cityName = Console.ReadLine();
        }

        return cities;
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
            default: break;
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
