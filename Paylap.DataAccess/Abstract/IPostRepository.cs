using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Abstract
{
    public interface IPostRepository
    {
        // C R U D
        Task<Post> CreatePost(Post post, int id);
        Task<List<Post>> GetAllPost(); // kullanıcının kendisi hariç olsun. olmasın?
        Task<List<Post>> GetPostByUserId(int userId);

        Task<Post> GetPostById(int id);
        Task<Post> UpdatePost(int id, string text);
        Task Delete(int id);
    }
}
