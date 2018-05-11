using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.DataAnnotations;

namespace Origins.API.Data.ChainStore
{
    public class AddInformationContent
    {
        [AliasAs("token")]
        public string Token { get; set; }
        [AliasAs("info")]
        public string Info { get; set; }
    }
}
