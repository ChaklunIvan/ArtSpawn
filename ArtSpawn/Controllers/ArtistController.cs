using ArtSpawn.Configurations.Headers;
using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using ArtSpawn.Models.Updates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Controllers
{
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<ArtistResponse>>> GetAllArtists([FromQuery]PagingRequest pagingRequest, CancellationToken cancellationToken)
        {
            var artists = await _artistService.FindAllAsync(pagingRequest, cancellationToken);

            return Ok(artists.Items).WithHeaders(PaginationHelper<ArtistResponse>.GetPagingHeaders(artists));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistResponse>> GetArtist(Guid id, CancellationToken cancellationToken)
        {
            var artist = await _artistService.FindAsync(id, cancellationToken);

            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistResponse>> CreateArtist([FromBody] ArtistRequest artistRequest, CancellationToken cancellationToken)
        {
            var artist = await _artistService.CreateAsync(artistRequest, cancellationToken);

            return Ok(artist);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistResponse>> UpdateArtist([FromBody] ArtistUpdate artistUpdate, CancellationToken cancellationToken)
        {
            var artist = await _artistService.UpdateAsync(artistUpdate, cancellationToken);

            return Ok(artist);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(Guid id, CancellationToken cancellationToken)
        {
            await _artistService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
