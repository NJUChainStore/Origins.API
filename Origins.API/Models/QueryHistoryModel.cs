using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Models
{
    public class QueryHistoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }
        public string Location { get; set; }
        public string Username { get; set; }
        public string ProductId { get; set; }
    }
}
