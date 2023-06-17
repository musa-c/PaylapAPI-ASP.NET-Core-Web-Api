using Paylap.Business.Abstract;
using Paylap.DataAccess.Abstract;
using Paylap.DataAccess.Concrete;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylap.Business.Concrete
{
    public class BookMarkService : IBookMarkService
    {
        private IBookMarkRepository _bookMarkRepository;

        public BookMarkService(IBookMarkRepository bookMarkRepository)
        {
            _bookMarkRepository = bookMarkRepository;
        }
        public async Task<BookMark> CreateBookMark(int userId, int postId)
        {
            return await _bookMarkRepository.CreateBookMark(userId, postId);
        }

        public async Task DeleteBookMark(int userId, int postId)
        {
           await _bookMarkRepository.DeleteBookMark(userId, postId);
        }

        public async Task<List<BookMark>> GetUserBookMarkByPostId(int id)
        {
            return await _bookMarkRepository.GetUserBookMarkByPostId(id);
        }
    }
}
