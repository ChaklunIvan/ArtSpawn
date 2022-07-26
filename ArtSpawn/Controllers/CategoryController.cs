using ArtSpawn.Extensions;
using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Constants;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<PagedList<CategoryResponse>>> GetAllCategories(PagingRequest pagingRequest ,CancellationToken cancellationToken)
        {
            var categories = await _categoryService.FindAllAsync(pagingRequest, cancellationToken);
            
            return Ok(categories.Items).WithHeaders(PaginationHelper<CategoryResponse>.GetPagingHeaders(categories), categories.Items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.FindAsync(id, cancellationToken);

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = RoleCostants.Artist + ", " + RoleCostants.Admin)]
        public async Task<ActionResult<CategoryResponse>> CreateCategory([FromBody]CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var category = await _categoryService.CreateAsync(categoryRequest, cancellationToken);

            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleCostants.Artist + ", " + RoleCostants.Admin)]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory([FromBody] CategoryUpdate categoryUpdate, CancellationToken cancellationToken)
        {
            var category = await _categoryService.UpdateAsync(categoryUpdate, cancellationToken);

            return Ok(category);
        }

        [HttpDelete]
        [Authorize(Roles = RoleCostants.Artist + ", " + RoleCostants.Admin)]
        public async Task<ActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
