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
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] UserRequest userRequest)
        {
            var result = await _userService.CreateAsync(userRequest);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            await _authenticationService.ValidateUserAsync(authenticationRequest);

            return Ok(new { Token = await _authenticationService.CreateTokenAsync() });
        } 
    }
   
}
