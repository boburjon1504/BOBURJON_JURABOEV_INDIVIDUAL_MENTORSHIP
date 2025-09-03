using FindWeather.BusinessLogic.Models;
using Refit;

namespace FindWeather.BusinessLogic.Refits;

public interface IGeoCodingApi
{
    [Get("/v1/search")]
    Task<GeoCodingResponse> SearchCityAsync([AliasAs("name")] string cityName);
}
