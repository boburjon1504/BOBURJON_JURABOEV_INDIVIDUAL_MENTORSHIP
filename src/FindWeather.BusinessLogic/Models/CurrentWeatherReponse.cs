using System.Text.Json.Serialization;

namespace FindWeather.BusinessLogic.Models;

public class CurrentWeatherReponse
{
    [JsonPropertyName("current_weather")]
    public CurrentWeather CurrentWeather { get; set; }
}
