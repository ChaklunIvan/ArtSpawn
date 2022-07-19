using ArtSpawn.Database;
using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest productRequest, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productRequest);
            var result = await _context.Products.AddAsync(product, cancellationToken);
            result.Entity.File = new byte[1];
            await _context.SaveChangesAsync(cancellationToken);

            var productResponse = _mapper.Map<ProductResponse>(result.Entity);

            return productResponse;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) ??
                throw new NotFoundException($"Product with id: {id} was not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<PagedList<ProductResponse>> FindAllAsync(PagingRequest pagingRequest)
        {
            var products = _context.Products.OrderBy(a => a.Id).AsQueryable();

            var (items, count) = PaginationHelper<Product>.ToPagedList(products, pagingRequest.PageNumber, pagingRequest.PageSize);

            var mapped = _mapper.Map<IEnumerable<ProductResponse>>(items);

            var productResponse = PaginationHelper<ProductResponse>.GetPagedModel(mapped, count, pagingRequest.PageNumber, pagingRequest.PageSize);

            return Task.FromResult(productResponse);
        }

        public async Task<ProductResponse> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) ??
                throw new NotFoundException($"Product with id: {id} was not found");

            var productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
        }

        public Task<PagedList<ProductResponse>> FindByAsync(PagingRequest pagingRequest, Expression<Func<Product, bool>> expression)
        {
            var products = _context.Products.Where(expression).OrderBy(a => a.Id).AsQueryable();

            var (items, count) = PaginationHelper<Product>.ToPagedList(products, pagingRequest.PageNumber, pagingRequest.PageSize);

            var mapped = _mapper.Map<IEnumerable<ProductResponse>>(items);

            var productResponse = PaginationHelper<ProductResponse>.GetPagedModel(mapped, count, pagingRequest.PageNumber, pagingRequest.PageSize);

            return Task.FromResult(productResponse);
        }

        public async Task<ProductResponse> UpdateAsync(ProductUpdate productUpdate, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(productUpdate);
            var result = _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);

            var productResponse = _mapper.Map<ProductResponse>(result.Entity);

            return productResponse;
        }
    }
}
