using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateAsync(UserRequest userRequest);
    }
}
