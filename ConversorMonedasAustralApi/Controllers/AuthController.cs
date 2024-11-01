using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthRequestDto credentials)
        {
            // Paso 1: Validar las credenciales y generar el token
            var token = _userService.Authenticate(credentials);

            if (token == null)
            {
                return Unauthorized(new { Message = "Credenciales incorrectas." });
            }

            // Paso 2: Retornar el token
            return Ok(new { Token = token });
        }
    }
}


