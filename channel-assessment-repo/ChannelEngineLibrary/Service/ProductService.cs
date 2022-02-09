namespace ChannelEngineLibrary.Service
{
    using ChannelEngineLibrary.ApiClient;
    using ChannelEngineLibrary.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    public sealed class ProductService : IProductService
    {
        private readonly IApiClient apiClient;

        public ProductService(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IEnumerable<ProductDto>> GetTop5ProductFromOrders()
        {
            var orderModel = await this.apiClient.GetInprogressOrders();

            if (orderModel == null) 
            {
                throw new Exception("Internal Server Error");
            }

            if (orderModel.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                throw new Exception("Bad request");
            }

            if (orderModel.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while getting orders");
            }

            var lines = orderModel.Content.SelectMany(x => x.Lines);

            IEnumerable<ProductDto> top5Products = this.GetTop5Products(lines);

            foreach (var p in top5Products)
            {
                ApiResponseModel<IEnumerable<Product>> apiResponse = await this.apiClient.GetProductByProductNo(p.MerchantProductNo);
                var product = apiResponse.Content.First();
                p.Stock = product.Stock;
                p.Name = product.Name;
                p.Gtin = product.Ean;
            }

            return top5Products;
        }

        public async Task<PostProductDto> UpdateProductStock(string productNo) 
        {
            ApiResponseModel<IEnumerable<Product>> productResponse = await this.apiClient.GetProductByProductNo(productNo);

            if (productResponse.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                throw new Exception("Bad request");
            }

            if (productResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while getting products");
            }

            Product product = productResponse.Content.First();

            product.Stock = 25;

            ApiResponseModel<PostProductDto> postProductResponse =  await this.apiClient.PostProduct(product);

            if (postProductResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while updating products");
            }

            return postProductResponse.Content;
        }

        private IEnumerable<ProductDto> GetTop5Products(IEnumerable<Line> lines) 
        {
            List<ProductDto> productResponses = lines.GroupBy(
                l => l.MerchantProductNo,
                (key, l) => new ProductDto
                {
                    MerchantProductNo = key,
                    TotalQuantity = l.Select(l => l.Quantity).Aggregate((a, b) => a + b),
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .ToList();
            
            return productResponses;
        }
    }
}
