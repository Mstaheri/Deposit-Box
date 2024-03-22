using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace WebSite.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{UserName}")]
        public async Task<IActionResult> Get([FromRoute] string UserName)
        {
            var result = await _userService.GetAsync(UserName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost()]
        public async Task<IActionResult> Insert([FromBody] User user)
        {
            var result = await _userService.AddAsync(user);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), "User", new { userName = user.UserName }, Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var result = await _userService.UpdateAsync(user);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{UserName}")]
        public async Task<IActionResult> Delete([FromRoute] string UserName)
        {
            var result = await _userService.DeleteAsync(UserName);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
