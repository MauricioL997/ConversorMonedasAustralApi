using Common.DTO;
using Common.Enum;
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
        [HttpGet("active")]
        public IActionResult GetAllActiveUsers()
        {
            var users = _userService.GetAllActiveUsers();
            return Ok(users);
        }

        // Obtener todos los usuarios inactivos
        [HttpGet("inactive")]
        public IActionResult GetAllInactiveUsers()
        {
            var users = _userService.GetAllInactiveUsers();
            return Ok(users);
        }

        // Obtener usuario por ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado." });
            }
            return Ok(user);
        }

        // Obtener usuario por nombre de usuario
        [HttpGet("username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound(new { Message = "Usuario no encontrado." });
            }
            return Ok(user);
        }

        // Actualizar usuario
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] UserDto userDto)
        {
            try
            {
                bool updated = _userService.UpdateUser(userDto);
                if (!updated)
                {
                    return NotFound(new { Message = "Usuario no encontrado para actualizar." });
                }
                return Ok(new { Message = "Usuario actualizado exitosamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // Soft delete del usuario
        [HttpDelete("soft-delete/{username}")]
        public IActionResult SoftDeleteUser(string username)
        {
            bool deleted = _userService.SoftDeleteUser(username);
            if (!deleted)
            {
                return NotFound(new { Message = "Usuario no encontrado para eliminación." });
            }
            return Ok(new { Message = "Usuario eliminado lógicamente." });
        }

        // Restaurar usuario eliminado
        [HttpPut("undelete/{username}")]
        public IActionResult UndeleteUser(string username)
        {
            bool restored = _userService.UndeleteUser(username);
            if (!restored)
            {
                return NotFound(new { Message = "Usuario no encontrado para restauración." });
            }
            return Ok(new { Message = "Usuario restaurado exitosamente." });
        }

        // Actualizar la suscripción del usuario
        [HttpPut("update-subscription/{userId}")]
        public IActionResult UpdateUserSubscription(int userId, [FromBody] SubscriptionType newType)
        {
            bool updated = _userService.UpdateUserSubscription(userId, newType);
            if (!updated)
            {
                return NotFound(new { Message = "Usuario no encontrado para actualizar la suscripción." });
            }
            return Ok(new { Message = "Suscripción del usuario actualizada exitosamente." });
        }
    }
}
