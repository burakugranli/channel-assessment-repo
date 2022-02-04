namespace ChannelEngineLibrary.Service
{
    using ChannelEngineLibrary.ApiClient;
    using ChannelEngineLibrary.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            if (orderModel.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("An error occurred while getting orders");
            }

            IEnumerable<Line> lines = this.GetLines(orderModel.Content);

            IEnumerable<ProductResponse> products = this.LinesToProducts(lines);

            IEnumerable<ProductResponse> top5 = this.GetTop5Products(products);

            return top5;
        }

        public async Task<ApiResponseModel<PostProductResponse>> UpdateProductStock(string productNo) 
        {
            ApiResponseModel<Product> productResponse = await this.apiClient.GetProductByProductNo(productNo);

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

            return postProductResponse;
        }

        private IEnumerable<ProductResponse> GetTop5Products(IEnumerable<ProductResponse> products) 
        {
            return products.OrderByDescending(x => x.TotalQuantity).Take(5).ToList();
        }

        private IEnumerable<ProductResponse> LinesToProducts(IEnumerable<Line> lines)
        {
            var products = new Dictionary<string, ProductResponse>();

            foreach (Line line in lines)
            {
                if (products.ContainsKey(line.MerchantProductNo))
                {
                    ProductResponse p = products[line.MerchantProductNo];
                    p.TotalQuantity += line.Quantity;
                    products[line.MerchantProductNo] = p;
                }
                else
                {
                    products[line.MerchantProductNo] = new ProductResponse
                    {
                        MerchantProductNo = line.MerchantProductNo,
                        Name = line.Description,
                        Gtin = line.Gtin,
                        TotalQuantity = line.Quantity
                    };
                }
            }

            return products.Values.ToList();
        }


        private IEnumerable<Line> GetLines(IEnumerable<Order> orders)
        {
            var lines = new List<Line>();

            foreach (Order order in orders) 
            {
                lines.AddRange(order.Lines);
            }
            return lines;
        }
    }
}
