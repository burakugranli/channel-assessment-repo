namespace ChannelEngineWebApp.Controllers
{
    using ChannelEngineLibrary.Model;
    using ChannelEngineLibrary.Service;
    using ChannelEngineWebApp.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductDto> productResponses = await this.productService.GetTop5ProductFromOrders();
            return View(productResponses.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string productNo)
        {
            PostProductDto response = await this.productService.UpdateProductStock(productNo);
            if (response.AcceptedCount > 0)
            {
                TempData["msg"] = string.Format($"<script>alert('Product {productNo} is updated');</script>");
                return RedirectToAction("Index");
            }

            TempData["msg"] = string.Format($"<script>alert('Product {productNo} cannot be updated');</script>");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
