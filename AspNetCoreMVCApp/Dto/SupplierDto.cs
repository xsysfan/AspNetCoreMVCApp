using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMVCApp.Dto
{
    public class SupplierDto
    {
        public int SupplierId { get; internal set; }
        public string SupplierName { get; internal set; }
        public string SupplierAddress { get; internal set; }
    }
}
