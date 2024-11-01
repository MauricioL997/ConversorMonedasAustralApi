using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class CurrencyDto
    {
        public required string Code { get; set; } // Código de la moneda (e.g., USD, EUR)

        public required string Legend { get; set; } // Descripción o leyenda de la moneda

        public required string Symbol { get; set; } // Símbolo de la moneda (e.g., $, €)

        public required decimal ConvertibilityIndex { get; set; } // Índice de convertibilidad
    }
}
