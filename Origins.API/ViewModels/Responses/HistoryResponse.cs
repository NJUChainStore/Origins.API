using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.ViewModels.Responses
{
    public class HistoryItemViewModel
    {
        public string ProductId { get; set; }
        public string ProduceDate { get;set }
    }

    public class HistoryResponse
    {
        public List<HistoryItemViewModel> ProductShortList { get; set; }
    }
}
