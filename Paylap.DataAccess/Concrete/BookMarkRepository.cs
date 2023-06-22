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
    public class BookMarkRepository : IBookMarkRepository
    {
        public async Task<BookMark> CreateBookMark(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var bookmark = await paylapDbContext.BookMarks.FirstOrDefaultAsync(a => a.PostId == postId && a.UserId == userId);
                if (bookmark != null)
                {
                    await DeleteBookMark(userId, postId);
                }
                else
                {
                    BookMark _bookMark = new() { UserId = userId, PostId = postId };
                    await paylapDbContext.AddAsync(_bookMark);
                    await paylapDbContext.SaveChangesAsync();
                    return _bookMark;
                }
            }
            return null; // 204
        }

        public async Task DeleteBookMark(int userId, int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var bookmark = await paylapDbContext.BookMarks.Where(a => a.UserId == userId & a.PostId == postId).SingleAsync();
                paylapDbContext.BookMarks.Remove(bookmark);
                await paylapDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<BookMark>> GetUserBookMarkByPostId(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var bookmark = await paylapDbContext.BookMarks.Where(p => p.PostId == id).Include(a => a.User)
                    .Select(p => new BookMark
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
                if (bookmark.Count == 0)
                    throw new ArgumentNullException("bookmark", "Yer imi bulunamadı.");
                return bookmark;
            }
        }
    }
}
