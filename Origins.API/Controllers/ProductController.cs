using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Origins.API.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Controllers
{
    [Route("/Product")]
    public class ProductController
    {
        [HttpPost]
        [Authorize]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Info saved successfully")]
        public async Task<IActionResult> Save([FromBody] ProductInfoCreateViewModel info)
        {
            return null;
        }

        [HttpGet("history")]
        [Authorize]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Gets query history of current user")]
        public async Task<IActionResult> GetShortHistory([FromBody] ProductInfoCreateViewModel info)
        {
            return null;
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation]
        [SwaggerResponse(200, description: "Query history deleted")]
        public async Task<IActionResult> DeleteQueryHistory([FromQuery]string productId)
        {
            return null;
        }

        [HttpGet]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(ProductInfoQueryViewModel), description: "Query a product")]
        public async Task<IActionResult> Query([FromQuery]string productId)
        {
            return null;
        }

        [HttpGet("QRCode")]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(ProductInfoQueryViewModel), description: "Product Queried")]
        public async Task<IActionResult> QRCode([FromQuery]QRScanParameters parameters)
        {
            return null;
        }
        
    }
}
