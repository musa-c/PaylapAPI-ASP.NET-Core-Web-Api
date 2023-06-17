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
    public class CommentRepository : ICommentRepository
    {
        public async Task<Comment> CreateComment(Comment comment)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                Comment _comment = new() { UserId = comment.UserId, PostId = comment.PostId, Text= comment.Text};
                await paylapDbContext.AddAsync(_comment);
                await paylapDbContext.SaveChangesAsync();
                return _comment;
            }
        }

        public async Task DeleteComment(int commentId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var bookmark = await paylapDbContext.Comments.Where(a => a.Id == commentId).SingleAsync();
                paylapDbContext.Comments.Remove(bookmark);
                await paylapDbContext.SaveChangesAsync();
            }
        }
        
        public async Task<Comment> GetCommentById(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                return await paylapDbContext.Comments.FindAsync(id);
            }
        }
    }
}
