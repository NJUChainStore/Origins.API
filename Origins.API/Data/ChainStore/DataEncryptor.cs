using Origins.API.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Data.ChainStore
{
    public class DataEncryptor
    {
        private readonly ChainStoreConfig config;

        public DataEncryptor(ChainStoreConfig config)
        {
            this.config = config;
        }

        public string Encrypt(string info)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(info);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public string Decrypt(string encrypted)
        {

            byte[] bytes = Convert.FromBase64String(encrypted);
            return System.Text.Encoding.Default.GetString(bytes);
        }


    }        
}
