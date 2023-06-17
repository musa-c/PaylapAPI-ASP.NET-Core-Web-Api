using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.DataAccess.Abstract
{
    public interface IBookMarkRepository
    {
        Task<BookMark> CreateBookMark(int userId, int postId);
        Task<List<BookMark>> GetUserBookMarkByPostId(int id);
        Task DeleteBookMark(int userId, int postId);
    }
}
