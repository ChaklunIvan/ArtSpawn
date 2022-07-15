using ArtSpawn.Configurations.Headers;
using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ProductResponse>>> GetAllProduct([FromQuery] PagingRequest pagingRequest, CancellationToken cancellationToken)
        {
            var products = await _productService.FindAllAsync(pagingRequest, cancellationToken);

            return Ok(products.Items).WithHeaders(PaginationHelper<ProductResponse>.GetPagingHeaders(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productService.FindAsync(id, cancellationToken);

            return Ok(product);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<PagedList<ProductResponse>>> GetProductsByCategory([FromQuery] PagingRequest pagingRequest, Guid id, CancellationToken cancellationToken)
        {
            var products = await _productService.FindAllAsync(pagingRequest, cancellationToken);
            
            return Ok(products.Items.Where(i => i.CategoryId == id))
                .WithHeaders(PaginationHelper<ProductResponse>.GetPagingHeaders(products));
        }

        [HttpPost("artists/{id}")]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] ProductRequest productRequest, Guid id, CancellationToken cancellationToken)
        {
            productRequest.ArtistId = id;
            var product = await _productService.CreateAsync(productRequest, cancellationToken);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] ProductUpdate productUpdate, CancellationToken cancellationToken)
        {
            var product = await _productService.UpdateAsync(productUpdate, cancellationToken);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await _productService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
