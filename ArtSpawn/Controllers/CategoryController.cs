using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistResponse>>> GetAllCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryService.FindAllAsync(cancellationToken);
            
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.FindAsync(id, cancellationToken);

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody]CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var category = await _categoryService.CreateAsync(categoryRequest, cancellationToken);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory([FromBody] CategoryUpdate categoryUpdate, CancellationToken cancellationToken)
        {
            var category = await _categoryService.UpdateAsync(categoryUpdate, cancellationToken);

            return Ok(category);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
