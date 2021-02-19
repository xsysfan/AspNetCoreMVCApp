using AspNetCoreMVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            using (var context = new ProductsDatabaseContext())
            {
                var userInfo = context.SalesPeoples.FirstOrDefault(x => x.Login == HttpContext.User.Identity.Name);
                if (userInfo != null)
                {
                    var productsList = new List<SelectListItem>();

                    foreach (var item in context.Products)
                    {
                        productsList.Add(new SelectListItem() { Value = item.ProductId.ToString(), Text = item.ProductName });
                    }

                    return View(new SalesEntryFormModel() { SalesmanLogin = userInfo.Login, CurrentDate = DateTime.Now.ToString("yyyy/MM/dd"), SalesPersonId = userInfo.SellerId, ProductList = productsList });
                }
                else
                {
                    return RedirectToAction("OptOut", "Home");
                }
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index([FromBody] SalesEntryFormModel inputData)
        {
            using (var context = new ProductsDatabaseContext())
            {
                var productInfo = context.Products.FirstOrDefault(x => x.ProductId == inputData.ProductId);
                if (productInfo != null)
                {
                    context.Sales.Add(
                        new Sale()
                        {
                            Amount = inputData.Amount,
                            ProductId = inputData.ProductId,
                            SellerId = inputData.SalesPersonId,
                            TransactionDate = DateTime.UtcNow,
                            Price = inputData.Price
                        }
                    );

                    return RedirectToAction("ProductsList", "Sales");
                }
            }

            return View(inputData);
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
