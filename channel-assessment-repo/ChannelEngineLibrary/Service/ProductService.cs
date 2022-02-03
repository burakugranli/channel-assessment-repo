using ChannelEngineLibrary.ApiClient;
using ChannelEngineLibrary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineLibrary.Service
{
    public sealed class ProductService : IProductService
    {
        private readonly IApiClient apiClient;

        public ProductService(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IEnumerable<Product>> GetTop5ProductFromOrders()
        {
            var orderModel = await this.apiClient.GetInprogressOrders();

            var lines = GetLines(orderModel.Content);

            var products = this.LinesToProducts(lines);

            var top5 = GetTop5Products(products);

            return top5;
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
