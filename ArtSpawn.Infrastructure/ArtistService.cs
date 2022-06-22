using ArtSpawn.Database;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }
        //ArtistRequest, ArtistResponse
        public async Task<Artist> CreateAsync(Artist artist, CancellationToken cancellationToken)
        {
            var result = await _context.Artists.AddAsync(artist, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var artist = await FindAsync(id, cancellationToken);
            _context.Remove(artist);
            await _context.SaveChangesAsync(cancellationToken);
        }
        //findall pageble
        public Task<List<Artist>> FindAllAsync(CancellationToken cancellationToken)
        {
            var artists = _context.Artists.ToListAsync(cancellationToken);
            return artists;
        }

        public async Task<Artist> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var artist = await _context.Artists.FindAsync(id, cancellationToken);
            return artist;
        }

        public async Task<Artist> UpdateAsync(Artist artist, CancellationToken cancellationToken)
        {
            var result = _context.Update(artist);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }
    }
}
