using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> CreateAsync(CategoryRequest categoryRequest, CancellationToken cancellationToken);
        Task<CategoryResponse> UpdateAsync(CategoryUpdate categoryUpdate, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedList<CategoryResponse>> FindAllAsync(PagingRequest pagingRequest, CancellationToken cancellationToken);
        Task<CategoryResponse> FindAsync(Guid id, CancellationToken cancellationToken);
    }
}
