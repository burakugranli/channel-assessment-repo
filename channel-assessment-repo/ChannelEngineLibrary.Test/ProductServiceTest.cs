namespace ChannelEngineLibrary.Test
{
    using ChannelEngineLibrary.ApiClient;
    using ChannelEngineLibrary.Model;
    using ChannelEngineLibrary.Service;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ProductServiceTest
    {
        [Fact]
        public async void GetTop5ProductFromOrdersTest_HappyPath()
        {
            // Arrange

            List<string> expectedTopProductNos = new List<string>
            {
                "1", "8", "7", "4", "2"
            };
            
            var orders = this.CreateOrderList();
            ApiResponseModel<Order> response = new ApiResponseModel<Order>
            {
                Content = orders,
                StatusCode = System.Net.HttpStatusCode.OK,
                Success = true
            };

            Mock<IApiClient> apiClientMock = new Mock<IApiClient>();

            apiClientMock.Setup(x => x.GetInprogressOrders()).ReturnsAsync(response);

            IProductService productService = new ProductService(apiClientMock.Object);

            // Act
            IEnumerable<ProductResponse> actualResponse = await productService.GetTop5ProductFromOrders();
            IList<ProductResponse> actualList = actualResponse.ToList();

            // Assert
            Assert.Equal(5, actualList.Count());

            for (int i= 0; i < expectedTopProductNos.Count(); i++)
            {
                Assert.Equal(expectedTopProductNos[i], actualList[i].MerchantProductNo);
            }
        }

        [Fact]
        public async void GetTop5ProductFromOrdersTest_Only3Product()
        {
            // Arrange

            List<string> expectedTopProductNos = new List<string>
            {
                "1", "3", "2"
            };

            var orders = this.CreateOrderListWith3Products();
            ApiResponseModel<Order> response = new ApiResponseModel<Order>
            {
                Content = orders,
                StatusCode = System.Net.HttpStatusCode.OK,
                Success = true
            };

            Mock<IApiClient> apiClientMock = new Mock<IApiClient>();

            apiClientMock.Setup(x => x.GetInprogressOrders()).ReturnsAsync(response);

            IProductService productService = new ProductService(apiClientMock.Object);

            // Act
            IEnumerable<ProductResponse> actualResponse = await productService.GetTop5ProductFromOrders();
            IList<ProductResponse> actualList = actualResponse.ToList();

            // Assert
            Assert.Equal(expectedTopProductNos.Count(), actualList.Count());

            for (int i = 0; i < expectedTopProductNos.Count(); i++)
            {
                Assert.Equal(expectedTopProductNos[i], actualList[i].MerchantProductNo);
            }
        }

        private IEnumerable<Order> CreateOrderList() 
        {
            

            Order order1 = new Order
            {
                Lines = new List<Line> 
                {
                    new Line {MerchantProductNo = "1", Description = "dummy desc 1", Quantity = 1, Gtin = "1" },
                    new Line {MerchantProductNo = "2", Description = "dummy desc 2", Quantity = 2, Gtin = "112312" },
                    new Line {MerchantProductNo = "3", Description = "dummy desc 3", Quantity = 2, Gtin = "12342" },
                    new Line {MerchantProductNo = "4", Description = "dummy desc 4", Quantity = 6, Gtin = "345341" }
                }
            };

            Order order2 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "1", Description = "dummy desc 1", Quantity = 10, Gtin = "234243" },
                    new Line {MerchantProductNo = "5", Description = "dummy desc 5", Quantity = 2, Gtin = "2453453" },
                    new Line {MerchantProductNo = "6", Description = "dummy desc 6", Quantity = 4, Gtin = "456465" }
                    
                }
            };

            Order order3 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "2", Description = "dummy desc 2", Quantity = 3, Gtin = "234243" },
                    new Line {MerchantProductNo = "7", Description = "dummy desc 7", Quantity = 8, Gtin = "142532453" },
                    new Line {MerchantProductNo = "8", Description = "dummy desc 8", Quantity = 9, Gtin = "4567843" }

                }
            };

            Order order4 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "4", Description = "dummy desc 4", Quantity = 1, Gtin = "67575" },
                    new Line {MerchantProductNo = "5", Description = "dummy desc 5", Quantity = 1, Gtin = "089484" }
                }
            };

            IList<Order> orders = new List<Order>
            {
                order1,
                order2,
                order3,
                order4
            };

            return orders;
        }

        private IEnumerable<Order> CreateOrderListWith3Products()
        {


            Order order1 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "1", Description = "dummy desc 1", Quantity = 1, Gtin = "1" },
                    new Line {MerchantProductNo = "2", Description = "dummy desc 2", Quantity = 2, Gtin = "112312" },
                    new Line {MerchantProductNo = "3", Description = "dummy desc 3", Quantity = 2, Gtin = "12342" }
                }
            };

            Order order2 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "1", Description = "dummy desc 1", Quantity = 10, Gtin = "234243" }
                }
            };

            Order order3 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "3", Description = "dummy desc 2", Quantity = 3, Gtin = "234243" }
                }
            };

            Order order4 = new Order
            {
                Lines = new List<Line>
                {
                    new Line {MerchantProductNo = "1", Description = "dummy desc 4", Quantity = 1, Gtin = "67575" },
                    new Line {MerchantProductNo = "2", Description = "dummy desc 5", Quantity = 1, Gtin = "089484" }
                }
            };

            IList<Order> orders = new List<Order>
            {
                order1,
                order2,
                order3,
                order4
            };

            return orders;
        }
    }
}
