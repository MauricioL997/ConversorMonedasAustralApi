using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? Authenticate(string Name, string password);
        List<User> GetAllActiveUsers();
        List<User> GetAllInactiveUsers();
        User GetUserById(int id);
        int AddUser(User user);
        User GetUserByUsername(string Username);
        bool UpdateUser(User user);
        bool SoftDeleteUser(string Username);
        bool UndeleteUser(string username);
        bool UpdateUserSubscription(int userId, int subscriptionId);
    }
}
