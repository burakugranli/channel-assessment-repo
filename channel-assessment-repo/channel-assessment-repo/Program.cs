using ChannelEngineLibrary.ApiClient;
using ChannelEngineLibrary.Business;
using ChannelEngineLibrary.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace channel_assessment_repo
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            IServiceCollection serviceCollection = Initialize();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ApplicationBusiness client = serviceProvider.GetService<ApplicationBusiness>();

            await client.RunAsync();

        }

        private static IServiceCollection Initialize()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<ApplicationBusiness>();
            serviceCollection.AddSingleton<IApiClientConfiguration, ApiClientConfiguration>();
            serviceCollection.AddSingleton<IApiClient, ApiClient>();

            return serviceCollection;
        }
    }
}
