using AspNetCoreMVCApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace AspNetCoreMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AuthorizationPage()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AuthorizationPage(LoginPageModel model)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.UserName)
            };

            var userIdentities = new ClaimsIdentity(userClaims, "ApplicationUserIdentity");

            var userPricipal = new ClaimsPrincipal(new[] { userIdentities });

            using (var context = new ProductsDatabaseContext())
            {
                var user = context.SalesPeoples.FirstOrDefault(x => x.Login == model.UserName && x.Password == model.Password);

                if (user != null)
                {
                    HttpContext.SignInAsync(userPricipal);

                    return RedirectToAction("Index", "Sales");
                }
            }

            return RedirectToAction("NotAuthorized");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult OptOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("AuthorizationPage");
        }

        [Authorize]
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
