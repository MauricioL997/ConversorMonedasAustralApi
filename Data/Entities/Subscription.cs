using Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public SubscriptionType Type { get; set; } // Tipo de suscripción (Free, Trial, Pro)

        [Required]
        public bool MonthlyReset { get; set; } // Indica si el límite se reinicia mensualmente
    }
}
