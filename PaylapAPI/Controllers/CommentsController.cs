using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paylap.Business.Abstract;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //[HttpGet]

    }
}
