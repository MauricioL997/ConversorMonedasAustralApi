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
        User Authenticate(string Name, string password);
        int RegisterUser(UserDto userDto);
    }
}
