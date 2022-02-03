using ChannelEngineLibrary.ApiClient;
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

            IApiClient client = serviceProvider.GetService<IApiClient>();

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
            serviceCollection.AddSingleton<IApiClientConfiguration, ApiClientConfiguration>();
            serviceCollection.AddSingleton<IApiClient, ApiClient>();

            return serviceCollection;
        }
    }
}
