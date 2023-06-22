using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Abstract
{
    public interface IUserRepository
    {
        // C R U D
        Task<User> CreateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> EmailOrPasswordCheck(string email, string password);
        Task<User> UserNameOrPasswordCheck(string username, string password);
        Task<User> UpdateUserName(int id, string username);
        Task<User> UpdateFirstName(int id, string firstname);
        Task<User> UpdateLastName(int id, string lastname);
        Task<User> UpdateEmail(int id, string email);
        Task<User> UpdatePassword(int id, string password);
        Task<User> UpdateAvatar(int id, string avatar);
        Task DeleteUser(int id);
    }
}
