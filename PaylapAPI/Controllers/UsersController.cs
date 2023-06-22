using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylap.Business.Abstract;
using Paylap.Entities;

namespace PaylapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        static string JsonConvertNotNull(List<User> users)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(users, Formatting.Indented, jsonSettings);
            return json;
        }

        static string JsonConvertNotNull(User user)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(user, Formatting.Indented, jsonSettings);
            return json;
        }

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createUser = await _userService.CreateUser(user);
            return CreatedAtAction("Get", new { id = createUser.Id }, user); // 201 + data
            // 1. parametre http eylem yönteminin adı.
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();
            return Ok(JsonConvertNotNull(users)); // 200 + data
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("[action]/{username}/{password}")]
        public async Task<IActionResult> UserNameOrPasswordCheck(string username, string password)
        {
            var user = await _userService.UserNameOrPasswordCheck(username, password);
            return Ok(JsonConvertNotNull(user));
        }

        [HttpGet]
        [Route("[action]/{email}/{password}")]
        public async Task<IActionResult> EmailOrPasswordCheck(string email, string password)
        {
            var user = await _userService.EmailOrPasswordCheck(email, password);
            return Ok(JsonConvertNotNull(user));
        }

        [HttpPut]
        [Route("[action]/{id}/{username}")]
        public async Task<IActionResult> UpdateUserName(int id, string username)
        {
            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdateUserName(id, username);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}/{firstname}")]
        public async Task<IActionResult> UpdateFirstName(int id, string firstname)
        {
            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdateFirstName(id, firstname);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}/{lastname}")]
        public async Task<IActionResult> UpdateLastName(int id, string lastname)
        {

            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdateLastName(id, lastname);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}/{email}")]
        public async Task<IActionResult> UpdateEmail(int id, string email)
        {

            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdateEmail(id, email);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}/{password}")]
        public async Task<IActionResult> UpdatePassword(int id, string password)
        {

            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdatePassword(id, password);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("[action]/{id}/{avatar}")]
        public async Task<IActionResult> UpdateAvatar(int id, string avatar)
        {

            if (await _userService.GetUserById(id) != null)
            {
                var user = await _userService.UpdateAvatar(id, avatar);
                return Ok(JsonConvertNotNull(user));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _userService.GetUserById(id) != null)
            {
                await _userService.DeleteUser(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
