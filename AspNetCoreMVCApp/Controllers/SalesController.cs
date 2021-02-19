using AspNetCoreMVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Data.Entities;
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

        [Authorize]
        public IActionResult SalesReport()
        {
            using (var context = new ProductsDatabaseContext())
            {
                var result = from sales in context.Sales
                             join products in context.Products on sales.ProductId equals products.ProductId
                             join salespeople in context.SalesPeoples on sales.SellerId equals salespeople.SellerId
                             select new SalesReportItemModel()
                             {
                                 TransactionId = sales.TransactionId,
                                 TransactionDate = sales.TransactionDate,
                                 ProductId = sales.ProductId,
                                 ProductName = products.ProductName,
                                 SellerId = sales.SellerId,
                                 SellerName = salespeople.SellerName,
                                 Amount = sales.Amount,
                                 Price = sales.Price,
                                 Total = sales.Amount * sales.Price
                             };

                var model = new SalesReportModel() { SalesItems = result.ToList() };

                return View(model);
            }
        }

        [Authorize]
        public IActionResult ProductsList()
        {
            return View();
        }

        [Authorize]
        public IActionResult Suppliers()
        {
            return View();
        }

    }
}
