using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pedidos.Functions.Configurations;
using System;
using System.Threading.Tasks;

namespace Pedidos.Functions;
public static class Program
{
    static async Task Main()
    {
        try
        {
            var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                   .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: false);
            })
            .ConfigureServices((context, services) =>
            {
                var appConfig = context.Configuration.LoadConfiguration();

                services.AddMemoryCache()
                        .AddRepositories(appConfig)
                        .AddUseCases();

            })
            .Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}