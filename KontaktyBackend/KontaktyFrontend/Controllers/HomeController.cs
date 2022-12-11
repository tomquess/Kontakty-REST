using KontaktyFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace KontaktyFrontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7188/api");
        HttpClient client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            List<KontaktViewModel> list = new List<KontaktViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Kontakt").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<KontaktViewModel>>(data);
            }
            return View(list);
        }

        public ActionResult Details()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}