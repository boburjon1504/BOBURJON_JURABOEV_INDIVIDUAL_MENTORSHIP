using FindWeather.BusinessLogic.Refits;
using FindWeather.BusinessLogic.Services;
using FindWeather.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FindWeather.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration) 
    {
        services
            .AddRefitClients(configuration)
            .AddServices();

        return services;
    }

    private static IServiceCollection AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IOpenMeteoApi>()
                            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("OpenMeteo") ?? string.Empty));

        services.AddRefitClient<IGeoCodingApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetValue<string>("GeoCoding") ?? string.Empty));

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWeatherProvider, WeatherProvider>();   

        return services;
    }
}
