using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Configs
{
    public class ChainStoreConfig
    {
        public bool UseMock { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
    }
}
