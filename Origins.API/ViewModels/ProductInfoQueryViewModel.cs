using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.ViewModels
{
    public class ProductInfoQueryViewModel
    {
                public string ProductId { get; set; }
        public string ProductDetails { get; set; }
        public DateTime Date { get; set; }
        public string Operator { get; set; }
    }
}
