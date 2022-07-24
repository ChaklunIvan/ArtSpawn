using ArtSpawn.Models.Constants;
using ArtSpawn.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ArtSpawn.Infrastructure.Helpers
{
    public static class TokenOptionsHelper
    {
        public static SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public static Task<List<Claim>> GetClaimsAsync(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            return Task.FromResult(claims);
        }

        public static JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
                (
                issuer: jwtSettings.GetSection("ValidIssuer").Value,
                audience: jwtSettings.GetSection("ValidAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

    }
}
