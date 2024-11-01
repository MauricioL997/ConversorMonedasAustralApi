using Common.Enum;

namespace Common.DTO
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public SubscriptionType Type { get; set; } // Tipo de suscripción (Free, Trial, Pro)
        public int ConversionLimit { get; set; } // Límite de conversiones
        public bool MonthlyReset { get; set; } // Indica si se reinicia mensualmente
    }
}
