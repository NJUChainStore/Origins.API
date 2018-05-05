using Origins.API.DataServices.Models;
using Origins.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.DataServices
{
    public interface IProductDataService
    {
        Task LoadDetail(ProductInfoModel model);

        Task AddInfo(ProductInfoCreateModel model);

        IQueryable<ProductInfoModel> Raw { get; }
    }
}
