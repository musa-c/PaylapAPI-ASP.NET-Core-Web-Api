using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylap.Business.Abstract;
using Paylap.Entities;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DislikesController : ControllerBase
    {
        private IDislikeService _dislikeService;

        public DislikesController(IDislikeService dislikeService)
        {
            _dislikeService = dislikeService;
        }

        static string JsonConvertNotNull(List<Dislike> posts)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented, jsonSettings);
            return json;
        }

        static string JsonConvertNotNull(Dislike post)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(post, Formatting.Indented, jsonSettings);
            return json;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserDislikesByPostId(int id)
        {
            var likes = await _dislikeService.GetUserDislikesByPostId(id);
            return Ok(JsonConvertNotNull(likes));
        }

        [HttpPost]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> CreateDislike(int userId, int postId)
        {
            var like = await _dislikeService.CreateDislike(userId, postId);
            return Ok(like);
        }

        [HttpDelete]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> DeleteDislike(int userId, int postId)
        {
            await _dislikeService.DeleteDislike(userId, postId);
            return Ok();
        }
    }
}
