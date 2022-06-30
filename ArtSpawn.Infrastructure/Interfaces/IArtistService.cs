using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface IArtistService
    {
        Task<ArtistResponse> CreateAsync(ArtistRequest artistRequest, CancellationToken cancellationToken);
        Task<ArtistResponse> UpdateAsync(ArtistUpdate artistUpdate, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PagedList<ArtistResponse>> FindAllAsync(PagingRequest pagingRequest, CancellationToken cancellationToken);
        Task<ArtistResponse> FindAsync(Guid id, CancellationToken cancellationToken);
    }
}
