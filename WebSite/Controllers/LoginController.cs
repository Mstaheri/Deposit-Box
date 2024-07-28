using Application.Services.Users.Queries.GetByUserNameAndPassword;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebSite.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : BaseController
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GetByUserNameAndPasswordQuery getByUserNameAndPasswordQuery
            , CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(getByUserNameAndPasswordQuery , cancellationToken);
            if (result.IsSuccess == true)
            {
                if (result.Data != null)
                {
                    var claims = new List<Claim>()
                    {
                       new Claim("UserName" , result.Data.UserName),
                       new Claim("Name" , result.Data.FirstName +" "+ result.Data.LastName)
                    };
                    string key = _configuration["JWTConfig:Key"];
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWTConfig:issuer"],
                        audience: _configuration["JWTConfig:audience"],
                        expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWTConfig:expires"])),
                        notBefore: DateTime.Now,
                        claims: claims,
                        signingCredentials: credentials);
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(jwtToken);
                }
                else
                {
                    return BadRequest("Sorry the credentials you are using are invalid");
                }
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
