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
        [Authorize(Roles = UserRole.Producer)]
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

        [HttpGet("History")]
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

        [HttpDelete("History")]
        [Authorize(Roles = UserRole.Client)]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Delete query history of productId")]
        public async Task<IActionResult> DeleteQueryHistory([FromQuery]string productId)
        {
            string username = HttpContext.User.Identity.Name;
            await queryHistoryService.DeleteHistoryAsync(username, productId);
            await queryHistoryService.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(ProductInfoQueryViewModel), description: "Product Queried. Returns the information of the product")]
        public async Task<IActionResult> Query([FromQuery]QRScanParameters parameters)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string username = HttpContext.User.Identity.Name;
                await queryHistoryService.DeleteHistoryAsync(username, parameters.ProductId);
                await queryHistoryService.AddHistoryAsync(new Models.QueryHistoryModel()
                {
                    Location = parameters.Location,
                    ProductId = parameters.ProductId,
                    Username = username
                });
                await queryHistoryService.SaveChangesAsync();
            }


            var allDetails = await productDataService.FindProduct(parameters.ProductId);

            return Ok(new ProductInfoQueryViewModel()
            {
                ProductId = parameters.ProductId,
                ProductDetails = allDetails.Select(x => new ProductInfoItem()
                {
                    Date = x.Date,
                    Detail = x.Detail,
                    Operator = x.Operator
                })
            });
        }


    }
}
