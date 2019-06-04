using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LegalTrucking.IntakePlus.Web.Ui.Membership;
using LegalTrucking.IntakePlus.Web.Ui.Models.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegalTrucking.IntakePlus.Web.Ui.Controllers
{
    public class DashboardController : Controller
    {
        private IHttpContextAccessor _context;

        public DashboardController(IHttpContextAccessor context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.UserName = _context.HttpContext.User.FindFirstValue("name");
            return View(model);
        }
    }
}