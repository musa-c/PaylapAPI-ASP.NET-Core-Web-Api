using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Business.Abstract
{
    public interface IBookMarkService
    {
        Task<BookMark> CreateBookMark(int userId, int postId);
        Task<List<BookMark>> GetUserBookMarkByPostId(int id);
        Task DeleteBookMark(int userId, int postId);
    }
}
