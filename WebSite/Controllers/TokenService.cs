using Application.Services.Users.Queries.GetByUserNameAndPassword;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace WebSite.Controllers
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTConfig:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWTConfig:expires")),
                SigningCredentials = creds,
                Issuer = _configuration["JWTConfig:issuer"],
                Audience = _configuration["JWTConfig:audience"]
            };

            var handler = new JsonWebTokenHandler();
            return handler.CreateToken(token);
        }

       
    }
}
