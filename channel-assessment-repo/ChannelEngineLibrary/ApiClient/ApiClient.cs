namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Configuration;
    using ChannelEngineLibrary.Model;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class ApiClient : IApiClient
    {
        private readonly IApiClientConfiguration configuration;

        public ApiClient(IApiClientConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<ApiResponseModel<IEnumerable<Order>>> GetInprogressOrders()
        {
            var restClient = new RestClient();

            string uri = string.Format("{0}orders?statuses={1}&apikey={2}", this.configuration.ApiUrl, "IN_PROGRESS", this.configuration.ApiKey);
            var restRequest = new RestRequest(uri, Method.Get);
            restRequest.AddHeader("Content-Type", "application/json");

            var response = await restClient.ExecuteGetAsync(restRequest);

            var responseContent = JsonConvert.DeserializeObject<ApiResponseModel<IEnumerable<Order>>>(response.Content);

            return responseContent;
        }

        public async Task<ApiResponseModel<IEnumerable<Product>>> GetProductByProductNo(string productNo) 
        {
            var restClient = new RestClient();

            string uri = string.Format("{0}products?search={1}&apikey={2}", this.configuration.ApiUrl, productNo, this.configuration.ApiKey);
            var restRequest = new RestRequest(uri, Method.Get);
            restRequest.AddHeader("Content-Type", "application/json");

            var response = await restClient.ExecuteGetAsync(restRequest);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var products = JsonConvert.DeserializeObject<ApiResponseModel<IEnumerable<Product>>>(response.Content, settings);

            return products;
        }

        public async Task<ApiResponseModel<PostProductResponse>> PostProduct(Product product)
        {
            var restClient = new RestClient();

            string uri = string.Format("{0}products?apikey={1}", this.configuration.ApiUrl, this.configuration.ApiKey);
            var restRequest = new RestRequest(uri, Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddBody(new Product[] { product });

            var response = await restClient.ExecutePostAsync(restRequest);

            var products = JsonConvert.DeserializeObject<ApiResponseModel<PostProductResponse>>(response.Content);

            return products;
        }

    }
}
