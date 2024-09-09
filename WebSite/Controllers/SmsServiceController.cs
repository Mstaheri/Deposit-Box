using Application.Services.SmsService.Queries.SendSms;
using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SmsServiceController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> SendSms([FromBody] SendSmsQuery sendSmsQuery,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(sendSmsQuery, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
