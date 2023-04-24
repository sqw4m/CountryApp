using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CountryDataApplication.Models;
using RestSharp;

namespace CountryDataApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly RestClient client = new RestClient();
        public CountriesDbContext Context { get; set; }

        public HomeController(ILogger<HomeController> logger, CountriesDbContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Country(string country)
        {
            var request = new RestRequest(
                $"https://restcountries.eu/rest/v2/name/{country}",
                Method.GET,
                DataFormat.Json);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful && response.StatusCode.HasFlag(HttpStatusCode.OK))
            {
                JArray jsonArray = JArray.Parse(response.Content);
                return View(jsonArray[0].ToObject<CountryResponse>());
            }

            return RedirectToAction("CountryNotFound", new { name = country });
        }

        // вывод всех стран из БД
        public IActionResult Countries()
        {
            return View(Context.Countries.Include(c => c.City).Include(r => r.Region));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CountryResponse countryResponse)
        {
            City city = null;
            Region region = null;

            if (!Context.Cities.Any(x => x.Name == countryResponse.Capital))
            {
                city = new City { Name = countryResponse.Capital };
            }
            else
                city = Context.Cities.First(x => x.Name == countryResponse.Capital);
            if (!Context.Regions.Any(x => x.Name == countryResponse.Region))
            {
                region = new Region { Name = countryResponse.Region };
            }
            else
                region = Context.Regions.First(x => x.Name == countryResponse.Region);

            if (!Context.Countries.Any(x => x.NumericCode == countryResponse.NumericCode))
            {
                Country country = new Country
                {
                    Name = countryResponse.Name,
                    NumericCode = countryResponse.NumericCode,
                    City = city,
                    Area = countryResponse.Area,
                    Population = countryResponse.Population,
                    Region = region
                };

                Context.Countries.Add(country);
            }
            Context.SaveChanges();
            
            return RedirectToAction("Countries");
        }

        // не найдена страна
        public IActionResult CountryNotFound(string name)
        {
            var t = (1, name);

            return View(t);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
