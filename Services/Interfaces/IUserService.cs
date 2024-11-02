using Common.DTO;
using Common.Enum;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        string Authenticate(AuthRequestDto credentials);
        User ValidateUser(AuthRequestDto credentials);
        string GenerateJwtToken(User user);
        int RegisterUser(UserDto userDto);
        bool UpdateUser(UserDto userDto);
        bool SoftDeleteUser(string username);
        bool UndeleteUser(string username);
        List<UserDto> GetAllActiveUsers();
        List<UserDto> GetAllInactiveUsers();
        UserDto GetUserById(int id);
        UserDto GetUserByUsername(string username);
        bool UpdateUserSubscription(int userId, SubscriptionType newType);
    }
}
