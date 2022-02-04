namespace ChannelEngineConsoleApp
{
    using ChannelEngineConsoleApp.Business;
    using ChannelEngineLibrary.ApiClient;
    using ChannelEngineLibrary.Configuration;
    using ChannelEngineLibrary.Service;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        public static async Task Main(string[] args)
        {
            IServiceCollection serviceCollection = Initialize();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ApplicationBusiness business = serviceProvider.GetService<ApplicationBusiness>();

            await business.Run();
        }

        private static IServiceCollection Initialize()
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ApplicationBusiness>();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<IProductService, ProductService>();
            serviceCollection.AddSingleton<IApiClientConfiguration, ApiClientConfiguration>();
            serviceCollection.AddSingleton<IApiClient, ApiClient>();

            return serviceCollection;
        }
    }
}
