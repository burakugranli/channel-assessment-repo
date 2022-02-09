namespace ChannelEngineLibrary.Service
{
    using ChannelEngineLibrary.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetTop5ProductFromOrders();

        Task<PostProductDto> UpdateProductStock(string productNo);
    }
}
