using ArtSpawn.Extensions;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSpawn.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] UserRequest userRequest)
        {
            var result = await _userService.CreateAsync(userRequest);

            return result;
        }
    }
   
}
