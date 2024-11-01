using Common.Enum;
using Data.Context;
using Data.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public User? Authenticate(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password );
        }
        public List<User> GetAllActiveUsers()
        {
            return _context.Users.Where(u=>u.IsActive).ToList();
        }
        public List<User> GetAllInactiveUsers()
        {
            return _context.Users.Where(u => !u.IsActive).ToList();
        }

        // Obtener usuario por ID
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id && !u.IsActive );
        }

        // Obtener usuario por Username
        public User GetUserByUsername(string Username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == Username);
        }

        // Agregar un nuevo usuario
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        // Actualizar usuario existente
        public bool UpdateUser(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Role = user.Role;
                existingUser.Type = user.Type;
                existingUser.IsActive = user.IsActive;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // Eliminación lógica de usuario (soft delete)
        public bool SoftDeleteUser(string Username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Username);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // Restaurar usuario (undelete)
        public bool UndeleteUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && !u.IsActive);
            if (user != null)
            {
                user.IsActive = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateUserSubscription(int userId, SubscriptionType newType)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.Type = newType;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
