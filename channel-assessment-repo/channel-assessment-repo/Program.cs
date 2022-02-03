namespace channel_assessment_repo
{
    using ChannelEngineLibrary.ApiClient;
    using ChannelEngineLibrary.Configuration;
    using ChannelEngineLibrary.Service;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using System.IO;
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            IServiceCollection serviceCollection = Initialize();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ProductService client = serviceProvider.GetService<ProductService>();

            await client.GetTop5ProductFromOrders();

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
            serviceCollection.AddSingleton<ProductService>();
            serviceCollection.AddSingleton<IApiClientConfiguration, ApiClientConfiguration>();
            serviceCollection.AddSingleton<IApiClient, ApiClient>();

            return serviceCollection;
        }
    }
}
