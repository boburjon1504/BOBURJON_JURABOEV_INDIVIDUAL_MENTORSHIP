using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace FindWeather.BusinessLogic.Models;

public class Daily
{
    [JsonPropertyName("time")]
    public required List<string> Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public required List<double> Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public required List<double> Temperature2mMin { get; set; }
}
