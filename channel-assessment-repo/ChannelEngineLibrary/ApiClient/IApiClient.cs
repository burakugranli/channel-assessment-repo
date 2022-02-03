namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Model;
    using System.Threading.Tasks;

    public interface IApiClient
    {
        Task<ApiResponseModel<Order>> GetInprogressOrders();

        Task<ApiResponseModel<Product>> GetProductByProductNo(string productNo);
    }
}
