using System.Text.Json.Serialization;

namespace FindWeather.BusinessLogic.Models;

public class CurrentWeather
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("winddirection")]
    public double WindDirection { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("time")]
    public string Time {  get; set; }
}
