using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/UserAndNumberOfShare")]
    [ApiController]
    public class UserAndNumberOfShareController : ControllerBase
    {
        private readonly UserAndNumberOfShareService _userAndNumberOfShareService;
        public UserAndNumberOfShareController(UserAndNumberOfShareService userAndNumberOfShareService)
        {
            _userAndNumberOfShareService = userAndNumberOfShareService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShareService.GetAllAsync(cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("NameBank/{NameBankSafe}")]
        public async Task<IActionResult> GetNameBank([FromRoute] string NameBankSafe,
            CancellationToken cancellationToken)
        {

            var result = await _userAndNumberOfShareService.GetNameBankAsync(NameBankSafe, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("UserName/{UserName}")]
        public async Task<IActionResult> GetUserName([FromRoute] string UserName,
            CancellationToken cancellationToken)
        {

            var result = await _userAndNumberOfShareService.GetUserNameAsync(UserName, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{NameBankSafe}/{UserName}")]
        public async Task<IActionResult> GetNameBankAndUserName([FromRoute] string NameBankSafe , string UserName,
            CancellationToken cancellationToken)
        {

            var result = await _userAndNumberOfShareService.GetNameBankAndUserNameAsync(NameBankSafe , UserName , cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] UserAndNumberOfShare userAndNumberOfShare,
            CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShareService.AddAsync(userAndNumberOfShare, cancellationToken);
            if (result.Success)
            {
                string url = Url.Action(nameof(GetNameBankAndUserName), "userAndNumberOfShare",
                    new { nameBankSafe = userAndNumberOfShare.NameBankSafe.Value , userName = userAndNumberOfShare.UserName.Value }, Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserAndNumberOfShare userAndNumberOfShare,
            CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShareService.UpdateAsync(userAndNumberOfShare, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{NameBankSafe}/{UserName}")]
        public async Task<IActionResult> Delete([FromRoute] string NameBankSafe , string UserName,
            CancellationToken cancellationToken)
        {
            var result = await _userAndNumberOfShareService.DeleteAsync(NameBankSafe , UserName, cancellationToken);
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
