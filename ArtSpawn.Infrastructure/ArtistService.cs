using ArtSpawn.Database;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArtSpawn.Infrastructure
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ArtistService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArtistResponse> CreateAsync(ArtistRequest artistRequest, CancellationToken cancellationToken)
        {
            var artist = _mapper.Map<Artist>(artistRequest);
            var result = await _context.Artists.AddAsync(artist, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var artistResponse = _mapper.Map<ArtistResponse>(result.Entity);

            return artistResponse;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ??
                throw new NotFoundException($"Artist with id: {id} was not found");
            
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<PagedList<ArtistResponse>> FindAllAsync(PagingRequest pagingRequest, CancellationToken cancellationToken)
        {
            var artists = _context.Artists.OrderBy(a => a.Id).AsQueryable();

            var (items, count) = PaginationHelper<Artist>.ToPagedList(artists, pagingRequest.PageNumber, pagingRequest.PageSize);

            var mapped = _mapper.Map<IEnumerable<ArtistResponse>>(items);

            var artistsResponse = PaginationHelper<ArtistResponse>.GetPagedModel(mapped, count, pagingRequest.PageNumber, pagingRequest.PageSize);
             
            return Task.FromResult(artistsResponse);
        }

        public async Task<ArtistResponse> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id, cancellationToken) ??
                throw new NotFoundException($"Artist with id: {id} was not found");

            var artistResponse = _mapper.Map<ArtistResponse>(artist);

            return artistResponse;
        }

        public async Task<ArtistResponse> UpdateAsync(ArtistUpdate artistUpdate, CancellationToken cancellationToken)
        {
            var artist = _mapper.Map<Artist>(artistUpdate);
            var result = _context.Artists.Update(artist);

            await _context.SaveChangesAsync(cancellationToken);

            var artistResponse = _mapper.Map<ArtistResponse>(result.Entity);

            return artistResponse;
        }

    }
}
