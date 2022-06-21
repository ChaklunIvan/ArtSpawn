using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;

namespace ArtSpawn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public async void Get(IFormFile file, CancellationToken cancellationToken)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream,cancellationToken);
            var array = memoryStream.ToArray(); 
        }
    }
}
