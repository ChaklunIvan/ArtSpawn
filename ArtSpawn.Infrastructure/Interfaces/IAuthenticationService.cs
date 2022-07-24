using ArtSpawn.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUserAsync(AuthenticationRequest authenticationRequest);
        Task<string> CreateTokenAsync();
    }
}
