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
    public class LikeService : ILikeService
    {
        private ILikeRepository _likeRepository;
        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }
        public async Task<Like> CreateLike(int userId, int postId)
        {
            return await _likeRepository.CreateLike(userId, postId);
        }

        public async Task DeleteLike(int userId, int postId)
        {
            await _likeRepository.DeleteLike(userId, postId);
        }

        public async Task<Like> GetIsUserPostLike(int userId, int postId)
        {
            return await _likeRepository.GetIsUserPostLike(userId, postId);
        }

        public async Task<List<Like>> GetUserLikesByPostId(int id)
        {
            return await _likeRepository.GetUserLikesByPostId(id);
        }
    }
}
