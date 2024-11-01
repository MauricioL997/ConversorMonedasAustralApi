using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        // Endpoint para realizar una conversión
        [HttpPost("convercion")]
        public IActionResult PerformConversion([FromBody] ConversionDto request)
        {
            if (request == null || request.UserId <= 0 || request.Amount <= 0 ||
                string.IsNullOrEmpty(request.FromCurrency) || string.IsNullOrEmpty(request.ToCurrency))
            {
                return BadRequest("Datos de solicitud inválidos.");
            }

            try
            {
                var result = _conversionService.PerformConversion(request.UserId, request.FromCurrency, request.ToCurrency, request.Amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Endpoint para obtener el historial de conversiones de un usuario
        [HttpGet("history/{userId}")]
        public IActionResult GetUserConversions(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("ID de usuario inválido.");
            }

            try
            {
                var conversions = _conversionService.GetUserConversions(userId);
                return Ok(conversions);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
