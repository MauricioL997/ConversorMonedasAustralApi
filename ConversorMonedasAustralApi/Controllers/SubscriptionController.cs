using Common.DTO;
using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // Endpoint para obtener una suscripción por tipo
        [HttpGet("{type}")]
        public IActionResult GetSubscriptionByType(SubscriptionType type)
        {
            var subscription = _subscriptionService.GetSubscriptionByType(type);
            if (subscription == null)
                return NotFound(new { Message = "Tipo de suscripción no encontrado." });

            return Ok(subscription);
        }

        // Endpoint para obtener todas las suscripciones
        [HttpGet]
        public IActionResult GetAllSubscriptions()
        {
            var subscriptions = _subscriptionService.GetAllSubscriptions();
            return Ok(subscriptions);
        }

        // Endpoint opcional para obtener el límite de conversiones de un tipo específico de suscripción
        [HttpGet("limit/{type}")]
        public IActionResult GetConversionLimit(SubscriptionType type)
        {
            var limit = _subscriptionService.GetConversionLimit(type);
            return Ok(new { Type = type.ToString(), ConversionLimit = limit });
        }
    }
}
