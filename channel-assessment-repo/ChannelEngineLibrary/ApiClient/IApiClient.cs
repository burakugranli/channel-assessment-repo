namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Model;
    using System.Threading.Tasks;

    public interface IApiClient
    {
        Task<OrderResponseModel> GetInprogressOrders();
    }
}
