using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Abstract
{
    public interface ICommentRepository
    {
        Task<Comment> CreateComment(Comment comment);
        Task<Comment> GetCommentById(int commentId);
        Task DeleteComment(int id);
    }
}
