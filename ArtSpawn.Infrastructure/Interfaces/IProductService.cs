﻿using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> CreateAsync(ProductRequest productRequest, CancellationToken cancellationToken);
        Task<ProductResponse> UpdateAsync(ProductUpdate productUpdate, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedList<ProductResponse>> FindAllAsync(PagingRequest pagingRequest, CancellationToken cancellationToken);
        Task<ProductResponse> FindAsync(Guid id, CancellationToken cancellationToken);
    }
}