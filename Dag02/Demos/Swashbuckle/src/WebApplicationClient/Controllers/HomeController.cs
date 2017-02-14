using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApi;
using ProductsApi.Models;

namespace WebApplicationClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts() {
            ProductsApi.MyAPIs client = new MyAPIs(new Uri("http://localhost:5000"));
            IList<Product> result = await client.ApiProductsGetAsync();
            return View(nameof(HomeController.GetProducts), result);

        }
    }
}
