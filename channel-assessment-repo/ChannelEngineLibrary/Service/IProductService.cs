namespace ChannelEngineLibrary.Service
{
    using ChannelEngineLibrary.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetTop5ProductFromOrders();

        Task<ApiResponseModel<PostProductResponse>> UpdateProductStock(string productNo);
    }
}
