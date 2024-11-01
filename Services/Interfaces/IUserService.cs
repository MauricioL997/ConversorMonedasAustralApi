using Common.DTO;
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
    }
}
