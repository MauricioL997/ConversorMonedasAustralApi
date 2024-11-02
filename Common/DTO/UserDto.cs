using Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class UserDto
    {
        public required string UserName { get; set; } // Nombre de usuario

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; } // Correo electrónico

        [DataType(DataType.Password)]
        public string Password { get; set; } // Contraseña

        public required string FirstName { get; set; } // Nombre

        public required string LastName { get; set; } // Apellido

        public EnumUser Role { get; set; } = EnumUser.user; // Rol del usuario

        public SubscriptionType Type { get; set; } // Tipo de suscripción

        public DateTime SubscriptionStartDate { get; set; } = DateTime.UtcNow; // Fecha de inicio de la suscripción
    }
}
