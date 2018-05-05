using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.ViewModels.Responses
{
    public class MultipleErrorResponse : ErrorResponse
    {
        public IEnumerable<string> ErrorDescriptions { get; set; }
    }
}
