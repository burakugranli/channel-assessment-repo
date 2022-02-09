namespace ChannelEngineLibrary.ApiClient
{
    using ChannelEngineLibrary.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IApiClient
    {
        Task<ApiResponseModel<IEnumerable<Order>>> GetInprogressOrders();

        Task<ApiResponseModel<IEnumerable<Product>>> GetProductByProductNo(string productNo);

        Task<ApiResponseModel<PostProductDto>> PostProduct(Product product);
    }
}
