using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.ViewModels
{
    public class ProductInfoItem
    {
        public DateTime Date { get; set; }
        public string Operator { get; set; }
        public string Detail { get; set; }
    }

    public class ProductInfoQueryViewModel
    {
        public string ProductId { get; set; }
        public IEnumerable<ProductInfoItem> ProductDetails { get; set; }
    }
}
