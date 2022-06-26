using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, ILoggerService logger, IMapper mapper)
        {
            _artistService = artistService;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArtists(CancellationToken cancellationToken)
        {
            var artists = await _artistService.FindAllAsync(cancellationToken);
            return Ok(artists);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(Guid id, CancellationToken cancellationToken)
        {
            var artist = await _artistService.FindAsync(id, cancellationToken);
            return Ok(artist);
        }
        [HttpPost]
        public async Task<IActionResult> CreateArtist(Artist artistToCreate, CancellationToken cancellationToken)
        {
            var artist = await _artistService.CreateAsync(artistToCreate, cancellationToken);
            return Ok(artist);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(Guid id ,Artist artistToUpdate, CancellationToken cancellationToken)
        {
            var artist = await _artistService.UpdateAsync(artistToUpdate, cancellationToken);
            return Ok(artist);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(Guid id, CancellationToken cancellationToken)
        {
            await _artistService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
