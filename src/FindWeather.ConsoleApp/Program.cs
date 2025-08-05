using FindWeather.BusinessLogic;
using FindWeather.ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                    services.AddBusinessLogic(context.Configuration);

                    services.AddSingleton<AppUI>();
                })
                .Build();

using var scope = host.Services.CreateScope();

var app = scope.ServiceProvider.GetRequiredService<AppUI>();

await app.RunAsync();