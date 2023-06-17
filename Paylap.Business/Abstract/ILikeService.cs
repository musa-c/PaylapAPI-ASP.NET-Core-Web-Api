using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Business.Abstract
{
    public interface ILikeService
    {
        Task<Like> CreateLike(int userId, int postId);
        Task<List<Like>> GetUserLikesByPostId(int id);
        Task<Like> GetIsUserPostLike(int userId, int postId);
        Task DeleteLike(int userId, int postId);
    }
}
