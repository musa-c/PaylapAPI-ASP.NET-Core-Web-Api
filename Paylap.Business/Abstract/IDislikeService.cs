using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Business.Abstract
{
    public interface IDislikeService
    {
        Task<Dislike> CreateDislike(int userId, int postId); 
        Task<List<Dislike>> GetUserDislikesByPostId(int id);
        Task DeleteDislike(int userId, int postId);
    }
}
