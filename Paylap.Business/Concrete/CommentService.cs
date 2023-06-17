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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<Comment> CreateComment(Comment comment)
        {
            return await _commentRepository.CreateComment(comment);
        }

        public async Task DeleteComment(int id)
        {
            await _commentRepository.DeleteComment(id);
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _commentRepository.GetCommentById(commentId);
        }
    }
}
