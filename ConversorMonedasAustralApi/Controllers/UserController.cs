using Common.DTO;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Services;



namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
    
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest(new { Message = "El nombre de usuario y la contraseña son obligatorios." });
            }
            // Llama al servicio para registrar al usuario
            int userId = _userService.RegisterUser(userDto);
            return Ok();
            
        }
    }
}
