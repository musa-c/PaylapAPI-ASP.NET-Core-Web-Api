using Paylap.Business.Abstract;
using Paylap.DataAccess.Abstract;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Business.Concrete
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
               _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);   
        }

        public async Task<User> EmailOrPasswordCheck(string email, string password)
        {
           return await _userRepository.EmailOrPasswordCheck(email, password);
        }

        public async Task<User> UserNameOrPasswordCheck(string username, string password)
        {
            return await _userRepository.UserNameOrPasswordCheck(username, password);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> UpdateAvatar(int id, byte[] avatar)
        {
            return await _userRepository.UpdateAvatar(id, avatar);
        }

        public async Task<User> UpdateEmail(int id, string email)
        {
            return await _userRepository.UpdateEmail(id, email);
        }

        public async Task<User> UpdateFirstName(int id, string firstname)
        {
            return await _userRepository.UpdateFirstName(id, firstname);
        }

        public async Task<User> UpdateLastName(int id, string lastname)
        {
            return await _userRepository.UpdateLastName(id, lastname);
        }

        public async Task<User> UpdatePassword(int id, string password)
        {
            return await _userRepository.UpdatePassword(id, password);
        }

        public async Task<User> UpdateUserName(int id, string username)
        {
            return await _userRepository.UpdateUserName(id, username);
        }
    }
}
