using System.Text.Json.Serialization;

namespace FindWeather.BusinessLogic.Models;

public class GeoCodingResponse
{
    [JsonPropertyName("results")]
    public List<GeoResult> Results { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }
}
