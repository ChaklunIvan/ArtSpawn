using ArtSpawn.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface IArtistService
    {
        Task<Artist> CreateAsync(Artist artist, CancellationToken cancellationToken);
        Task<Artist> UpdateAsync(Artist artist, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Artist>> FindAllAsync(CancellationToken cancellationToken);
        Task<Artist> FindAsync(Guid id, CancellationToken cancellationToken);
    }
}
