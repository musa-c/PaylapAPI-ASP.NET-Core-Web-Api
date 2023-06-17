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
    public class DislikeService : IDislikeService
    {
        private IDislikeRepository _dislikeRepository;
        public DislikeService(IDislikeRepository dislikeRepository)
        {
            _dislikeRepository = dislikeRepository;
        }
        public async Task<Dislike> CreateDislike(int userId, int postId)
        {
            return await _dislikeRepository.CreateDislike(userId, postId);
        }

        public async Task DeleteDislike(int userId, int postId)
        {
            await _dislikeRepository.DeleteDislike(userId, postId);
        }

        public async Task<List<Dislike>> GetUserDislikesByPostId(int id)
        {
            return await _dislikeRepository.GetUserDislikesByPostId(id);
        }
    }
}
