namespace MarktguruApi.Controllers.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using Models.Authentication;

    /// <summary>
    /// Controller responsible for handling authorization and token generation.
    /// </summary>
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// The secret key used for token generation.
        /// </summary>
        private const string TokenSecret = "my_secret_key_12345_my_secret_key_12345_my_secret_key_12345";

        /// <summary>
        /// The lifetime of the generated token.
        /// </summary>
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);

        /// <summary>
        /// Generates a JWT token based on the provided user credentials.
        /// </summary>
        /// <param name="dto">The DTO containing the user credentials.</param>
        /// <returns>An IActionResult containing the generated JWT token or an error response.</returns>
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenGenerationDto dto)
        {
            if(string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest();
            }

            if(dto.UserName != "Test" || dto.Password != "test")
            {
                return Unauthorized();
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(TokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, dto.UserName),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            string? jwt = await Task.FromResult(tokenHandler.WriteToken(token));
            return Ok(jwt);
        }
    }
}