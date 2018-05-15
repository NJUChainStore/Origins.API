using Microsoft.Extensions.Options;
using Origins.API.Configs;
using Origins.API.Data.ChainStore;
using Origins.API.DataServices;
using Origins.API.DataServices.Models;
using Origins.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Data
{
    public class ProductDataController : IProductDataService
    {
        private readonly ChainStoreConfig config;
        private readonly IChainStoreService storeService;
        private readonly DataEncryptor encryptor;
        private readonly ApplicationContext context;

        public ProductDataController(IOptions<ChainStoreConfig> config, ApplicationContext context)
        {
            this.config = config.Value;
            this.storeService = WebApiClient.HttpApiClient.Create<IChainStoreService>(this.config.Url);
            this.encryptor = new DataEncryptor(this.config);
            this.context = context;
        }

        public IQueryable<ProductInfoModel> Raw => context.ProductInfo;

        public async Task AddInfo(ProductInfoCreateModel model)
        {
			AddInformationResponse response = null;
			if (!config.UseMock) {
				response = await storeService.AddInformationAsync(new AddInformationContent()
                {
                    Token = config.Token,
                    Info = encryptor.Encrypt(model.ProductDetails)
                });
			}
  

            var newInfo = new ProductInfoModel()
            {
                Id = Guid.NewGuid().ToString(),
				BlockIndex = config.UseMock ? 0 : response.BlockIndex,
				BlockOffset = config.UseMock ? 0 : response.Offset,
                Detail = config.UseMock ? model.ProductDetails : "",
                Date = DateTime.UtcNow,
                ProductId = model.ProductId,
                Operator = model.Operator
            };

            context.ProductInfo.Add(newInfo);
            await context.SaveChangesAsync();
            
        }

        public async Task LoadDetail(ProductInfoModel model)
        {
            if (config.UseMock)
            {
                return;
            }
            var res = await storeService.FindInformationAsync(model.BlockIndex, model.BlockOffset, config.Token);
            model.Detail = res.Info;
        }
    }
}
