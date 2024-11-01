using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using System.Collections.Generic;

namespace ConversorMonedasAustralApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        // Obtener todas las monedas
        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            List<CurrencyDto> currencies = _currencyService.GetAllCurrencies();
            return Ok(currencies);
        }

        // Obtener moneda por Id
        [HttpGet("{id}")]
        public IActionResult GetCurrencyById(int id)
        {
            var currency = _currencyService.GetCurrencyById(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        // Crear una nueva moneda
        [HttpPost]
        public IActionResult AddCurrency([FromBody] CurrencyDto currencyDto)
        {
            if (currencyDto == null || string.IsNullOrEmpty(currencyDto.Code))
            {
                return BadRequest();
            }

            int currencyId = _currencyService.AddCurrency(currencyDto);
            return CreatedAtAction(nameof(GetCurrencyById), new { id = currencyId, CurrencyId = currencyId });
        }

        // Actualizar una moneda existente
        [HttpPut("{id}")]
        public IActionResult UpdateCurrency(int id, [FromBody] CurrencyDto currencyDto)
        {
            bool success = _currencyService.UpdateCurrency(id, currencyDto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

        // Eliminar una moneda (lógica de eliminación)
        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            bool success = _currencyService.DeleteCurrency(id);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
