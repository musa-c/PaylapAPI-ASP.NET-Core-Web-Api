using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Abstract
{
    public interface ILikeRepository
    {
        Task<Like> CreateLike(int userId, int postId); // generic type
        Task<List<Like>> GetUserLikesByPostId(int id);
        Task<Like> GetIsUserPostLike(int userId, int postId);
        Task DeleteLike(int userId, int postId);
    }
}
