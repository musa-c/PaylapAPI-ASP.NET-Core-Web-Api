using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylap.Business.Abstract;
using Paylap.Entities;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private ILikeService _likeService;
        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        static string JsonConvertNotNull(List<Like> posts)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented, jsonSettings);
            return json;
        }

        static string JsonConvertNotNull(Like post)
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
        public async Task<IActionResult> GetUserLikesByPostId(int id)
        {
            var likes = await _likeService.GetUserLikesByPostId(id);
            return Ok(JsonConvertNotNull(likes));
        }

        [HttpGet]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> GetIsUserPostLike(int userId, int postId)
        {
            var likes = await _likeService.GetIsUserPostLike(userId, postId);
            return Ok(JsonConvertNotNull(likes));
        }

        [HttpPost]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> CreateLike(int userId, int postId)
        {
            var like = await _likeService.CreateLike(userId, postId);
            return Ok(like);
        }

        [HttpDelete]
        [Route("[action]/{userId}/{postId}")]
        public async Task<IActionResult> DeleteLike(int userId, int postId)
        {
            await _likeService.DeleteLike(userId, postId);
            return Ok();
        }
    }
}
