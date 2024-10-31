using Common.DTO;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
    
        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
        _configuration = configuration;
        _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthRequestDto credentials)
        {
            User? userAuthenticate = _userRepository.Authenticate(credentials.Username, credentials.Password);

            if (userAuthenticate is not null)
            {
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
                SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>
                {
                    new Claim("sub", userAuthenticate.Id.ToString()),
                    new Claim("given_name", userAuthenticate.Name)
                };

                var jwtSecurityToken = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signature);

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return Ok(tokenToReturn);
            }
            return Unauthorized();
        }
    }
}
