using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public required string Code { get; set; } // Código de la moneda (e.g., USD, EUR)

        [StringLength(100)]
        [Required]
        public required string Legend { get; set; } // Leyenda o descripción de la moneda

        [StringLength(10)]
        [Required]
        public required string Symbol { get; set; } // Símbolo de la moneda (e.g., $, €)

        [Required]
        public decimal ConvertibilityIndex { get; set; } // Índice de convertibilidad

        public bool IsActive { get; set; } = true; // Estado activo para eliminación lógica
    }
}
