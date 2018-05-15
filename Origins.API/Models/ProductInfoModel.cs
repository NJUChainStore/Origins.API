using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Models
{
    public class ProductInfoModel
    {
        [Key]
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string Operator { get; set; }

        //[NotMapped]
        public string Detail { get; set; }

        public DateTime Date { get; set; }
       
        public long BlockIndex { get; set; }
        
        public int BlockOffset { get; set; }


    }
}
