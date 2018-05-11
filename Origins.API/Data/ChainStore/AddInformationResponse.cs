using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.DataAnnotations;

namespace Origins.API.Data.ChainStore
{
    public class AddInformationResponse
    {
        [AliasAs("blockIndex")]
        public long BlockIndex { get; set; }
        [AliasAs("offset")]
        public int Offset { get; set; }
    }

}
