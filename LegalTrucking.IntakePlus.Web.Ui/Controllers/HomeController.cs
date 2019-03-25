using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LegalTrucking.IntakePlus.Web.Ui.Models;
using Microsoft.Extensions.Options;

namespace LegalTrucking.IntakePlus.Web.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<ApplicationSettings> config;

        public HomeController(IOptions<ApplicationSettings> config)
        {
            this.config = config;
        }

        // GET: /<controller>/
        public IActionResult Index() => View(config.Value);

        public IActionResult Privacy()
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
