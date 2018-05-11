using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.DataServices.Models
{
    public class ProductInfoCreateModel
    {
        public string ProductId { get; set; }
        public string ProductDetails { get; set; }
        public string Operator { get; set; }
    }
}
