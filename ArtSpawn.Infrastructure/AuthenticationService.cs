using ArtSpawn.Infrastructure.Helpers;
using ArtSpawn.Infrastructure.Interfaces;
using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User _user;
        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> ValidateUserAsync(AuthenticationRequest authenticationRequest)
        {
            _user = await _userManager.FindByNameAsync(authenticationRequest.UserName);
            var passwordCheck = await _userManager.CheckPasswordAsync(_user, authenticationRequest.Password);

            if (_user == null || passwordCheck == false)
                throw new UnauthorizedException("Authentication failed. Wrong user name or password");

            return true;
        }

        public async Task<string> CreateTokenAsync()
        {
            var signingCredentials = TokenOptionsHelper.GetSigningCredentials();
            var claims = await TokenOptionsHelper.GetClaimsAsync(_user, await _userManager.GetRolesAsync(_user));
            var tokenOptions = TokenOptionsHelper.GenerateTokenOptions(signingCredentials, claims, _configuration);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        
    }
}
