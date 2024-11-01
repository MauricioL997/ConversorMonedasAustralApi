using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Conversion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } 

        [Required]
        public required string FromCurrency { get; set; }

        [Required]
        public required string ToCurrency { get; set; }

        [Required]
        public decimal Amount { get; set; } 

        public decimal Result { get; set; } 

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public required virtual User User { get; set; }
    }
}
