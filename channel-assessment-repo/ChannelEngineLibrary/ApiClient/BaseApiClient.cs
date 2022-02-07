namespace ChannelEngineLibrary.ApiClient
{
    using RestSharp;
    using System.Threading.Tasks;

    public abstract class BaseApiClient
    {

        public async Task<RestResponse> Get(string uri)
        {
            var restClient = new RestClient();

            var restRequest = new RestRequest(uri, Method.Get);
            restRequest.AddHeader("Content-Type", "application/json");

            var response = await restClient.ExecuteGetAsync(restRequest);

            return response;
        }

        public async Task<RestResponse> Post(string uri, object body)
        {
            var restClient = new RestClient();

            var restRequest = new RestRequest(uri, Method.Post);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddBody(body);

            var response = await restClient.ExecutePostAsync(restRequest);

            return response;
        }
    }
}
