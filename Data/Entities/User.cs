using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enum;

namespace Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public required string UserName { get; set; } // Nombre de usuario

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public required string Email { get; set; } // Correo electrónico

        [StringLength(50)]
        [DataType(DataType.Password)]
        [Required]
        public required string Password { get; set; } // Contraseña

        [StringLength(50)]
        [Required]
        public required string FirstName { get; set; } // Nombre

        [StringLength(50)]
        [Required]
        public required string LastName { get; set; } // Apellido

        public EnumUser Role { get; set; } = EnumUser.user; // Rol del usuario (por defecto 'user')

        public SubscriptionType Type; // Tipo de suscripción

        public DateTime SubscriptionStartDate { get; set; } // Fecha de inicio de la suscripción

        public bool IsActive { get; set; } = true; // Estado activo para eliminaciones lógicas
    }
}
