using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylap.Business.Abstract;
using Paylap.Entities;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkersController : ControllerBase
    {
        private IBookMarkService _bookmarkService;
        public BookMarkersController(IBookMarkService bookMarkService)
        {
            _bookmarkService = bookMarkService;
        }

        static string JsonConvertNotNull(List<BookMark> posts)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented, jsonSettings);
            return json;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserBookMarkByPostId(int id)
        {
            var bookMarks = await _bookmarkService.GetUserBookMarkByPostId(id);
            return Ok(JsonConvertNotNull(bookMarks));
        }

        [HttpPost]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> CreateBookMark(int userId, int postId)
        {
            var bookMark = await _bookmarkService.CreateBookMark(userId, postId);
            return Ok(bookMark);
        }

        [HttpDelete]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> DeleteBookMark(int userId, int postId)
        {
            await _bookmarkService.DeleteBookMark(userId, postId);
            return Ok();
        }


    }
}
