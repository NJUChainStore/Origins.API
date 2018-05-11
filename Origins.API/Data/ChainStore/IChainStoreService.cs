using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace Origins.API.Data.ChainStore
{
    [HttpHost("http://www.webapiclient.com")]
    public interface IChainStoreService : IDisposable
    {
        [HttpPost("/data")]
        ITask<AddInformationResponse> AddInformationAsync([JsonContent] AddInformationContent content);

        [HttpGet("/data")]
        ITask<FindInformationResponse> FindInformationAsync(long blockIndex, int offset, string token);
    }


}
