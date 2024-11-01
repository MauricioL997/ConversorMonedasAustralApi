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
using Common.Enum;
using Microsoft.Extensions.Configuration;
using Data.Repositories.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public string Authenticate(AuthRequestDto credentials)
        {
            User user = ValidateUser(credentials);
            if (user == null)
            {
                return null; // Usuario no autenticado
            }

            return GenerateJwtToken(user);
        }
        public User ValidateUser(AuthRequestDto credentials)
        {
            return _repository.Authenticate(credentials.Username, credentials.Password);
        }
        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("sub", user.Id.ToString()),
        new Claim("email", user.Email),
        new Claim("given_name", user.FirstName),
        new Claim("family_name", user.LastName),
        new Claim("role", user.Role.ToString())
    };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }



        public int RegisterUser(UserDto userDto)
        {
            // Verificar si el usuario ya existe
            var existingUser = _repository.GetUserByUsername(userDto.UserName);
            if (existingUser != null)
            {
                throw new ArgumentException("El nombre de usuario ya está en uso.");
            }

            // Crear el nuevo usuario a partir del DTO y asignarle la suscripción Free
            var newUser = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Role = EnumUser.user,
                Type = SubscriptionType.Free,

                SubscriptionStartDate = DateTime.UtcNow,
                IsActive = true
            };

            // Guardar el usuario en el repositorio
            return _repository.AddUser(newUser);
        }
        // Crear el nuevo usuario a partir del DTO

    }
}

