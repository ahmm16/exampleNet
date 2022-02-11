using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public interface IUserServices
    {
        Task<List<User>> GetUsers();

        Task<User> AddUser(UserCreatedDto user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(Guid UserId);

        Task<User> GetUserById(Guid UserId);
        

    }
}
