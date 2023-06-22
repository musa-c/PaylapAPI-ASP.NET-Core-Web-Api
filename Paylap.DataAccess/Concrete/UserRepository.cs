using Microsoft.EntityFrameworkCore;
using Paylap.DataAccess.Abstract;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Paylap.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> CreateUser(User user)
        {
            if (IsEmailUnique(user.Email))
            {
                if (IsUserNameUnique(user.UserName))
                {
                    using (var paylapDbContext = new PaylapDbContext())
                    {
                        await paylapDbContext.Users.AddAsync(user);
                        await paylapDbContext.SaveChangesAsync();
                        return user;
                    }
                }
                else
                {
                    throw new Exception("kullanıcı adı zaten kullanılmaktadır.");
                }
            
            }
            else
            {
                throw new Exception("Email adresi zaten kullanılmaktadır.");
            }
        }

        private static bool IsEmailUnique(string email)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = paylapDbContext.Users.FirstOrDefault(u => u.Email == email);
                return (user == null);
            }
        }

        private static bool IsUserNameUnique(string userName) 
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = paylapDbContext.Users.FirstOrDefault(u => u.UserName == userName);
                return (user == null);
            }
        }


        public async Task DeleteUser(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var deleteUser = await GetUserById(id);
                paylapDbContext.Users.Remove(deleteUser);
                await paylapDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var users = await paylapDbContext.Users.Select(p => new User
                {
                    Id = p.Id,
                    FirstName = p.FirstName ?? "",
                    LastName = p.LastName ?? "",
                    UserName = p.UserName ?? "",
                    Password = p.Password,
                    Email = p.Email,
                    Avatar = p.Avatar ?? "237",
                }).ToListAsync();
                return users;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = await paylapDbContext.Users.Where(p => p.Id == id).
                    Select(a => new User
                {
                    Id = a.Id,
                    FirstName = a.FirstName ?? "",
                    BookMarkCount = a.BookMarks.Count,
                    LikeCount = a.Likes.Count,
                    DislikeCount = a.Dislikes.Count,
                    CommentCount = a.Comments.Count,
                    LastName = a.LastName ?? "",
                    UserName = a.UserName ?? "",
                    Password = a.Password,
                    Email = a.Email,
                    Avatar = a.Avatar ?? "237",
                }).SingleOrDefaultAsync();

                return user;
            }
        }

        public async Task<User> UpdateAvatar(int id, string avatar)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var updateUser = await GetUserById(id);
                updateUser.Avatar = avatar;
                paylapDbContext.Users.Update(updateUser);
                await paylapDbContext.SaveChangesAsync();
                return updateUser;
            }
        }

        public async Task<User> UpdateEmail(int id, string email)
        {
            if (IsEmailUnique(email))
            {
                using (var paylapDbContext = new PaylapDbContext())
                {
                    var updateUser = await GetUserById(id);
                    updateUser.Email = email;
                    paylapDbContext.Users.Update(updateUser);
                    await paylapDbContext.SaveChangesAsync();
                    return updateUser;
                }
            }
            else
            {
                throw new Exception("Email adresi zaten kullanılmaktadır.");
            }
         
        }

        public async Task<User> UpdateFirstName(int id, string firstname)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var updateUser = await GetUserById(id);
                updateUser.FirstName = firstname;
                paylapDbContext.Users.Update(updateUser);
                await paylapDbContext.SaveChangesAsync();
                return updateUser;
            }
        }

        public async Task<User> UpdateLastName(int id, string lastname)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var updateUser = await GetUserById(id);
                updateUser.LastName = lastname;
                paylapDbContext.Users.Update(updateUser);
                await paylapDbContext.SaveChangesAsync();
                return updateUser;
            }
        }

        public async Task<User> UpdatePassword(int id, string password)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var updateUser = await GetUserById(id);
                updateUser.Password = password;
                paylapDbContext.Users.Update(updateUser);
                await paylapDbContext.SaveChangesAsync();
                return updateUser;
            }
        }

        public async Task<User> UpdateUserName(int id, string username)
        {
            if (IsUserNameUnique(username))
            {
                using (var paylapDbContext = new PaylapDbContext())
                {
                    var updateUser = await GetUserById(id);
                    updateUser.UserName = username;
                    paylapDbContext.Users.Update(updateUser);
                    await paylapDbContext.SaveChangesAsync();
                    return updateUser;
                }
            }
            else
            {
                throw new Exception("kullanıcı adı zaten kullanılmaktadır.");
            }
       
        }

        //FirstOrDefaultAsync bulursa Users nesnesi bulamazsa null.
        public async Task<User> EmailOrPasswordCheck(string email, string password)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = await paylapDbContext.Users.Where(u => u.Email == email && u.Password == password)
                    .Select(u => new User
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Avatar = u.Avatar ?? "237",
                        FirstName = u.FirstName,
                        Email = u.Email,
                        Password = u.Password,
                    }).FirstOrDefaultAsync();

                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("Kullanıcı bulunamadı");
                }
            }

        }

        public async Task<User> UserNameOrPasswordCheck(string username, string password)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = await paylapDbContext.Users.Where(u => u.UserName == username && u.Password == password)
                    .Select(u => new User
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Avatar = u.Avatar ?? "237",
                        FirstName = u.FirstName,
                        Email = u.Email,
                        Password = u.Password,
                    }).SingleOrDefaultAsync();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new Exception("Kullanıcı bulunamadı");
                }
            }
        }
    }
}
