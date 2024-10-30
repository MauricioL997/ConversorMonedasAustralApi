using Data.Entities;
using Common.DTO;
using Data.Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public User Authenticate(string Name, string password)
        {
            return _repository.Authenticate(Name, password);
        }
        public int RegisterUser(UserDto userDto)
        {
            // Verificar si el usuario ya existe
            var existingUser = _repository.GetUserByUsername(userDto.UserName);
            if (existingUser != null)
            {
                throw new ArgumentException("El nombre de usuario ya está en uso.");
            }

            // Crear el nuevo usuario a partir del DTO
            var newUser = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                Name = userDto.Name,
                Email = userDto.Email,
                SubscriptionId = userDto.SubscriptionId
            };

            // Usar AddUser para registrar el usuario
            return _repository.AddUser(newUser);
        }

    }
}

