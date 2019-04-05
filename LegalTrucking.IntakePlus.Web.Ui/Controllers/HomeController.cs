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
    [Route("")]
    public class HomeController : Controller
    {      
        // GET: /<controller>/
        public IActionResult Index() => Redirect("/Account/Login");
    }
}
