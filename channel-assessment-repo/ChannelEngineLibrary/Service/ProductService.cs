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

        public async Task<IEnumerable<ProductResponse>> GetTop5ProductFromOrders()
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

            if (orderModel.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while getting orders");
            }

            var lines = orderModel.Content.SelectMany(x => x.Lines);

            IEnumerable<ProductResponse> top5 = this.GetTop5Products(lines);

            return top5;
        }

        public async Task<PostProductResponse> UpdateProductStock(string productNo) 
        {
            ApiResponseModel<IEnumerable<Product>> productResponse = await this.apiClient.GetProductByProductNo(productNo);

            if (productResponse.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                throw new Exception("Bad request");
            }

            if (productResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while getting products");
            }

            Product product = productResponse.Content.First();

            product.Stock = 25;

            ApiResponseModel<PostProductResponse> postProductResponse =  await this.apiClient.PostProduct(product);

            if (postProductResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while updating products");
            }

            return postProductResponse.Content;
        }

        private IEnumerable<ProductResponse> GetTop5Products(IEnumerable<Line> lines) 
        {
            List<ProductResponse> productResponses = lines.GroupBy(
                l => l.MerchantProductNo,
                (key, l) => new ProductResponse
                {
                    MerchantProductNo = key,
                    TotalQuantity = l.Select(l => l.Quantity).Aggregate((a, b) => a + b),
                })
                .OrderByDescending(p => p.TotalQuantity)
                .Take(5)
                .ToList();

            foreach (var productResp in productResponses)
            {
                Line line = lines.Where(l => l.MerchantProductNo == productResp.MerchantProductNo).FirstOrDefault();
                productResp.Gtin = line.Gtin;
                productResp.Name = line.Description;
            }

            return productResponses;
        }
    }
}
