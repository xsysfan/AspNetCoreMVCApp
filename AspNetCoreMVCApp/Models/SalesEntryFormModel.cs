using Microsoft.AspNetCore.Mvc.Rendering;
using Products.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMVCApp.Models
{
    public class SalesEntryFormModel
    {
        public int SalesPersonId { get; set; }
        public string SalesmanLogin { get; set; }
        public string CurrentDate { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
