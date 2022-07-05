using ArtSpawn.Database;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArtSpawn.Infrastructure.Helpers;

namespace ArtSpawn.Infrastructure
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CategoryResponse> CreateAsync(CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var result = await _context.Categories.AddAsync(category, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var categoryResponse = _mapper.Map<CategoryResponse>(result.Entity);

            return categoryResponse;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken) ??
                throw new NotFoundException($"Category with id: {id} was not found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<PagedList<CategoryResponse>> FindAllAsync(PagingRequest pagingRequest ,CancellationToken cancellationToken)
        {
            var categories = _context.Categories.OrderBy(c => c.Id).AsQueryable();

            var (items, count) = PaginationHelper<Category>.ToPagedList(categories, pagingRequest.PageNumber, pagingRequest.PageSize);

            var mapped = _mapper.Map<IEnumerable<CategoryResponse>>(items);

            var categoryResponses = PaginationHelper<CategoryResponse>.GetPagedModel(mapped, count, pagingRequest.PageNumber, pagingRequest.PageSize);

            return Task.FromResult(categoryResponses);
        }

        public async Task<CategoryResponse> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ??
                 throw new NotFoundException($"Category with id: {id} was not found");

            var categoryResponse = _mapper.Map<CategoryResponse>(category);

            return categoryResponse;
        }

        public async Task<CategoryResponse> UpdateAsync(CategoryUpdate categoryUpdate, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(categoryUpdate);
            var result = _context.Categories.Update(category);

            await _context.SaveChangesAsync(cancellationToken);

            var artistResponse = _mapper.Map<CategoryResponse>(result.Entity);

            return artistResponse;
        }
    }
}
