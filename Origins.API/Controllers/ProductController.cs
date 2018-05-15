using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Origins.API.DataServices;
using Origins.API.ViewModels;
using Origins.API.ViewModels.Responses;
using Origins.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IQueryHistoryDataService queryHistoryService;
        private readonly IProductDataService productDataService;

        public ProductController(IQueryHistoryDataService queryHistoryService, IProductDataService productDataService)
        {
            this.queryHistoryService = queryHistoryService;
            this.productDataService = productDataService;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Info saved successfully")]
        public async Task<IActionResult> Save([FromBody] ProductInfoCreateViewModel info)
        {
            await productDataService.AddInfo(new DataServices.Models.ProductInfoCreateModel()
            {
                ProductId = info.ProductId,
                ProductDetails = info.ProductDetails,
                Operator = HttpContext.User.Identity.Name
            });
            return Ok();
        }

        [HttpGet("history")]
        [Authorize(Roles = UserRole.Client)]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(HistoryResponse), description: "Gets query history of current user")]
        public async Task<IActionResult> GetShortHistory([FromBody] ProductInfoCreateViewModel info)
        {
            string username = HttpContext.User.Identity.Name;
            return Ok(new HistoryResponse()
            {
                ProductShortList = queryHistoryService.Raw.Where(x => x.Username == username)
                                   .Select(x => new HistoryItemViewModel()
                                   {
                                       ProductId = x.ProductId
                                   })
            });

        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Query history deleted")]
        public async Task<IActionResult> DeleteQueryHistory([FromQuery]string productId)
        {
            string username = HttpContext.User.Identity.Name;
            await queryHistoryService.RemoveWhereAsync(x => x.Username == username);
            return Ok();
        }

        private async Task<ProductInfoQueryViewModel> GetProductInfo(string productId)
        {
            var allDetails = productDataService.Raw
                .Where(x => x.ProductId == productId);

            await Task.WhenAll(allDetails.Select(x => productDataService.LoadDetail(x)));
 
            return new ProductInfoQueryViewModel()
            {
                ProductId = productId,
                ProductDetails = allDetails.Select(x => new ProductInfoItem()
                {
                    Date = x.Date,
                    Detail = x.Detail,
                    Operator = x.Operator
                })
            };
        }

        [HttpGet("/")]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(ProductInfoQueryViewModel), description: "Query a product")]
        public async Task<IActionResult> Query([FromQuery]string productId)
        {

            await queryHistoryService.AddAHistoryAsync(new Models.QueryHistoryModel()
            {
                Location = null,
                ProductId = productId,
                Username = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.Identity.Name : null
            });
            return Ok(await GetProductInfo(productId));
        }
         
        [HttpGet("QRCode")]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(ProductInfoQueryViewModel), description: "Product Queried")]
        public async Task<IActionResult> QRCode([FromQuery]QRScanParameters parameters)
        {
            await queryHistoryService.AddAHistoryAsync(new Models.QueryHistoryModel()
            {
                Location = parameters.Location,
                ProductId = parameters.ProductId,
                Username = HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.Identity.Name : null
            });
            return Ok(await GetProductInfo(parameters.ProductId));
        }
        
    }
}
