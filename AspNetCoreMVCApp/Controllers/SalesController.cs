using AspNetCoreMVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMVCApp.Controllers
{
    public class SalesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View(new SalesEntryFormModel() { SalesmanLogin = HttpContext.User.Identity.Name, CurrentDate = DateTime.Now.ToString("yyyy/MM/dd") });
        }
    }
}
