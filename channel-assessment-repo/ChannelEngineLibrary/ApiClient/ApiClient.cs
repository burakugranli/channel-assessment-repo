namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Configuration;
    using ChannelEngineLibrary.Model;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;

    public sealed class ApiClient : IApiClient
    {
        private readonly IApiClientConfiguration configuration;

        public ApiClient(IApiClientConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<OrderResponseModel> GetInprogressOrders()
        {
            var restClient = new RestClient();

            string uri = string.Format("{0}orders?statuses={1}&apikey={2}", this.configuration.ApiUrl, "IN_PROGRESS", this.configuration.ApiKey);
            var restRequest = new RestRequest(uri, Method.Get);
            restRequest.AddHeader("Content-Type", "application/json");

            var response = await restClient.ExecuteGetAsync(restRequest);

            var orders = JsonConvert.DeserializeObject<OrderResponseModel>(response.Content);

            return orders;
        }

    }
}
