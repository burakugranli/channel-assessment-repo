namespace ChannelEngineConsoleApp.Business
{
    using ChannelEngineLibrary.Model;
    using ChannelEngineLibrary.Service;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class ApplicationBusiness
    {
        private readonly IProductService productService;

        public ApplicationBusiness(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task Run()
        {
            IEnumerable<ProductResponse> productResponses = await this.productService.GetTop5ProductFromOrders();

            Console.WriteLine("Top 5 products");

            foreach (var product in productResponses)
            {
                Console.WriteLine($"Product No : {product.MerchantProductNo}, Product Name : {product.Name}, Product Gtin : {product.Gtin}, Total Quantity : {product.TotalQuantity}");
            }

            Console.ReadKey();
        }
    }
}
