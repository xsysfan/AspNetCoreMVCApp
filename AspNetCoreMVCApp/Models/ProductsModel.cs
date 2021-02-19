using AspNetCoreMVCApp.Dto;
using System.Collections.Generic;

namespace AspNetCoreMVCApp.Models
{
    public class ProductsModel
    {
        public List<ProductDto> Products { get; internal set; }
    }
}
