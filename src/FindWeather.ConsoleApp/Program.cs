

using FindWeather.BusinessLogic.Refits;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System.Text.Json;

var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    var path = Directory.GetCurrentDirectory();

                    Console.WriteLine(path);

                    config.SetBasePath(path);

                    config.AddJsonFile("config.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddRefitClient<IOpenMeteoApi>()
                            .ConfigureHttpClient(c => c.BaseAddress = new Uri(context.Configuration.GetValue<string>("OpenMeteo") ?? string.Empty));

                    services.AddRefitClient<IGeoCodingApi>()
                            .ConfigureHttpClient(c => c.BaseAddress = new Uri(context.Configuration.GetValue<string>("GeoCoding") ?? string.Empty));
                })
                .Build();

using var scope = host.Services.CreateScope();

var openMeteoClient = scope.ServiceProvider.GetRequiredService<IOpenMeteoApi>();

var geoCodingClient = scope.ServiceProvider.GetRequiredService<IGeoCodingApi>();


Console.Write("Enter city name: ");

var city = Console.ReadLine() ?? "Tashkent";

var result = await geoCodingClient.SearchCityAsync(city);

var weather = await openMeteoClient.GetCurrentTemperature(result.Results[0].Latitude, result.Results[0].Longitude);

Console.WriteLine(JsonSerializer.Serialize(weather));
Console.WriteLine(JsonSerializer.Serialize(result));
