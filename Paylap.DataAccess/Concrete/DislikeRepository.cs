using Microsoft.EntityFrameworkCore;
using Paylap.DataAccess.Abstract;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Concrete
{
    public class DislikeRepository : IDislikeRepository
    {
        public async Task<Dislike> CreateDislike(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var dislike = await paylapDbContext.Dislikes.FirstOrDefaultAsync(a => a.PostId == postId && a.UserId == userId);
                if (dislike != null)
                {
                    await DeleteDislike(userId, postId);
                }
                else
                {
                    Dislike _dislike = new() { UserId = userId, PostId = postId };
                    await paylapDbContext.AddAsync(_dislike);
                    await paylapDbContext.SaveChangesAsync();
                    return _dislike;
                }
            }
            return null; // 204
        }

        public async Task DeleteDislike(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var dislike = await paylapDbContext.Dislikes.Where(a => a.UserId == userId & a.PostId == postId).SingleAsync();
                paylapDbContext.Dislikes.Remove(dislike);
                await paylapDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Dislike>> GetUserDislikesByPostId(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                //var users = await paylapDbContext.Posts
                //      .Where(p => p.Id == id).Include(a => a.Likes).ToListAsync();
                var dislikes = await paylapDbContext.Dislikes.Where(p => p.PostId == id).Include(a => a.User)
                    .Select(p => new Dislike
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
                    }).ToListAsync();
                if (dislikes.Count == 0)
                    throw new ArgumentNullException("dislike", "dislike bulunamadı");
                return dislikes;
            }
        }
    }
}
