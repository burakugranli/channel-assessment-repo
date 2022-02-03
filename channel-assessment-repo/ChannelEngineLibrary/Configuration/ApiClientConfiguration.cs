namespace ChannelEngineLibrary.Configuration
{
    using Microsoft.Extensions.Configuration;

    public sealed class ApiClientConfiguration : IApiClientConfiguration
    {
        private readonly IConfiguration configuration;

        public ApiClientConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string ApiUrl
        {
            get
            {
                return this.configuration["ApiUrl"];
            }
        }

        public string ApiKey
        {
            get
            {
                return this.configuration["ApiKey"];
            }
        }
    }
}
