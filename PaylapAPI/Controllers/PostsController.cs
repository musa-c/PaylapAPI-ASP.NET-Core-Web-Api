using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylap.Business.Abstract;
using Paylap.Entities;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        static string JsonConvertNotNull(List<Post> posts)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented, jsonSettings);
            return json;
        }

        static string JsonConvertNotNull(Post post)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(post, Formatting.Indented, jsonSettings);
            return json;
        }

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> CreatePost([FromBody] Post post, int id)
        {
            await _postService.CreatePost(post, id);
            return Ok(JsonConvertNotNull(post));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllPost();
            return Ok(JsonConvertNotNull(posts));
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post != null)
            {
                return Ok(JsonConvertNotNull(post));
            }
            return NotFound();
        }

        // getpost username


        [HttpGet]
        [Route("[action]/{userId}")]
        public async Task<IActionResult> GetPostByUserId(int userId)
        {
            var post = await _postService.GetPostByUserId(userId);
            if (post != null)
            {
                return Ok(JsonConvertNotNull(post));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] string text)
        {
            if (await _postService.GetPostById(id) != null)
            {
                var post = await _postService.UpdatePost(id, text);
                return Ok(JsonConvertNotNull(post));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _postService.GetPostById(id) != null)
            {
                await _postService.Delete(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
