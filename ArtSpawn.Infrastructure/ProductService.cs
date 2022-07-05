using ArtSpawn.Database;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

            await _context.SaveChangesAsync(cancellationToken);

            var productResponse = _mapper.Map<ProductResponse>(result.Entity);

            return productResponse;
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ProductResponse>> FindAllAsync(PagingRequest pagingRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> UpdateAsync(ProductRequest productRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
