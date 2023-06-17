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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<Post> CreatePost(Post post, int id)
        {
            return await _postRepository.CreatePost(post, id);
        }

        public async Task Delete(int id)
        {
            await _postRepository.Delete(id);
        }

        public async Task<List<Post>> GetAllPost()
        {
            return await _postRepository.GetAllPost();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _postRepository.GetPostById(id);
        }

        public async Task<List<Post>> GetPostByUserId(int userId)
        {
            return await _postRepository.GetPostByUserId(userId);
        }

        public async Task<Post> UpdatePost(int id, string text)
        {
            return await _postRepository.UpdatePost(id, text);
        }
    }
}
