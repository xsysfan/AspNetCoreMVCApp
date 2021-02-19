using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMVCApp.Models
{
    public class SalesReportItemModel
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
