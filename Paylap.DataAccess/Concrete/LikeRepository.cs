using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Paylap.DataAccess.Abstract;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        public  async Task<Like> CreateLike(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var like = await paylapDbContext.Likes.FirstOrDefaultAsync(a => a.PostId == postId && a.UserId == userId);
                if (like != null )
                {
                    await DeleteLike(userId, postId);
                }
                else
                {
                    Like _like =  new() { UserId = userId, PostId = postId };
                    await paylapDbContext.AddAsync(_like);
                    await paylapDbContext.SaveChangesAsync();
                    return _like;
                }
            }
            return null; // 204
        }

        public async Task DeleteLike(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var like = await paylapDbContext.Likes.Where(a => a.UserId == userId & a.PostId == postId).SingleAsync();
                paylapDbContext.Likes.Remove(like);
                await paylapDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Like>> GetUserLikesByPostId(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var likes = await paylapDbContext.Likes.Where(p => p.PostId == id).Include(a => a.User)
                    .Select(p => new Like
                    {
                        Id = p.Id,
                        UserId = p.User.Id,
                        PostId = p.PostId,
                        User = new User
                        {
                            Id = p.User.Id,
                            UserName = p.User.UserName ?? "",
                            LastName= p.User.LastName ?? "",
                            FirstName= p.User.FirstName ?? "",
                            Avatar = p.User.Avatar ?? "237"
                        }
                    }).ToListAsync();
                if (likes.Count == 0)
                    throw new ArgumentNullException("like", "like bulunamadı.");
                return likes;
            }
        }

        public async Task<Like> GetIsUserPostLike(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var likes = await paylapDbContext.Likes.Where(p => p.PostId == postId & p.UserId == userId).Include(a => a.User).Select(p => new Like
                {
                    Id = p.Id,
                    UserId = p.User.Id,
                    PostId = p.PostId,
                    User = new User
                    {
                        Id = p.User.Id,
                        UserName = p.User.UserName ?? "",
                        LastName = p.User.LastName ?? "",
                        FirstName = p.User.FirstName ?? "",
                        Avatar = p.User.Avatar ?? "237"
                    }
                }).SingleAsync();
                    //throw new ArgumentNullException("like", "like bulunamadı.");
                return likes;
            }
        }
    }
}

