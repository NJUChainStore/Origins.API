using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.DataAnnotations;

namespace Origins.API.Data.ChainStore
{
    public class FindInformationResponse
    {
        [AliasAs("info")]
        public string Info { get; set; }
    }
}
