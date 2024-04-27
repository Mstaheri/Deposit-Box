using Application.Services;
using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.UpdateUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Queries.GetAllUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Queries.GetByNameBankAndUserName;
using Application.Services.UserAndNumberOfShares.Queries.GetByUserName;
using Application.Services.UserAndNumberOfShares.Queries.GetUserAndNumberOfShare;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/UserAndNumberOfShare")]
    [ApiController]
    public class UserAndNumberOfShareController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllUserAndNumberOfShareQuery = new GetAllUserAndNumberOfShareQuery();
            var result = await Mediator.Send(getAllUserAndNumberOfShareQuery, cancellationToken);
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
            var getByNameBankQuery = new GetByNameBankQuery()
            {
                NameBankSafe = NameBankSafe,
            };
            var result = await Mediator.Send(getByNameBankQuery, cancellationToken);
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
            var getByUserNameQuery = new GetByUserNameQuery()
            {
                UserName = UserName,
            };
            var result = await Mediator.Send(getByUserNameQuery, cancellationToken);
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
            var getByNameBankAndUserNameQuery = new GetByNameBankAndUserNameQuery()
            {
                NameBankSafe = NameBankSafe,
                UserName = UserName,
            };
            var result = await Mediator.Send(getByNameBankAndUserNameQuery, cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] AddUserAndNumberOfShareCommand addUserAndNumberOfShareCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addUserAndNumberOfShareCommand, cancellationToken);
            if (result.Success)
            {
                string url = Url.Action(nameof(GetNameBankAndUserName), "userAndNumberOfShare",
                    new { nameBankSafe = addUserAndNumberOfShareCommand.NameBankSafe ,
                        userName = addUserAndNumberOfShareCommand.UserName },
                    Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserAndNumberOfShareCommand updateUserAndNumberOfShareCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateUserAndNumberOfShareCommand, cancellationToken);
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
        public async Task<IActionResult> Delete([FromRoute] DeleteUserAndNumberOfShareCommand deleteUserAndNumberOfShareCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteUserAndNumberOfShareCommand, cancellationToken);
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
