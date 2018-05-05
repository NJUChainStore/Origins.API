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
        public IQueryable<ProductInfoModel> Raw => throw new NotImplementedException();

        public Task AddInfo(ProductInfoCreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task LoadDetail(ProductInfoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
