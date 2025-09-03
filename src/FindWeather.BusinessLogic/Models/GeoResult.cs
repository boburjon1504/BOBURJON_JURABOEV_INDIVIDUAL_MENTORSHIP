using System.Text.Json.Serialization;

namespace FindWeather.BusinessLogic.Models;

public class GeoResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("feature_code")]
    public string FeatureCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("admin1_id")]
    public int Admin1Id { get; set; }

    [JsonPropertyName("timezone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("country_id")]
    public int CountryId { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("admin1")]
    public string Admin1 { get; set; }
}