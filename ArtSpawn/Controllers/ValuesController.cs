using ArtSpawn.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ValuesController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        //[HttpGet("123")]
        //public async Task<IActionResult> GetArtist()
        //{
        //    var a = await _artistService.FindAllAsync();

        //    return Ok(a);
        //}
        public async void Get(IFormFile file, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream,cancellationToken);
            var array = memoryStream.ToArray(); 
        }
    }
}
