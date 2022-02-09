namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Configuration;
    using ChannelEngineLibrary.Model;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class ApiClient : BaseApiClient, IApiClient
    {
        private readonly IApiClientConfiguration configuration;

        public ApiClient(IApiClientConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ApiResponseModel<IEnumerable<Order>>> GetInprogressOrders()
        {
            string uri = string.Format("{0}orders?statuses={1}&apikey={2}", this.configuration.ApiUrl, "IN_PROGRESS", this.configuration.ApiKey);
            
            var response = await base.Get(uri);

            var responseContent = JsonConvert.DeserializeObject<ApiResponseModel<IEnumerable<Order>>>(response.Content);

            return responseContent;
        }

        public async Task<ApiResponseModel<IEnumerable<Product>>> GetProductByProductNo(string productNo) 
        {
            string uri = string.Format("{0}products?search={1}&apikey={2}", this.configuration.ApiUrl, productNo, this.configuration.ApiKey);

            var response = await base.Get(uri);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var products = JsonConvert.DeserializeObject<ApiResponseModel<IEnumerable<Product>>>(response.Content, settings);

            return products;
        }

        public async Task<ApiResponseModel<PostProductDto>> PostProduct(Product product)
        {
            string uri = string.Format("{0}products?apikey={1}", this.configuration.ApiUrl, this.configuration.ApiKey);

            var response =  await base.Post(uri, new Product[] { product });

            var products = JsonConvert.DeserializeObject<ApiResponseModel<PostProductDto>>(response.Content);

            return products;
        }

    }
}
