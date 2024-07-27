using Application.Services;
using Application.Services.Users.Commands.AddUser;
using Application.Services.Users.Commands.DeleteUser;
using Application.Services.Users.Commands.UpdateUser;
using Application.Services.Users.Queries.GetAllUser;
using Application.Services.Users.Queries.GetUser;
using Domain.Entity;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace WebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllUserCommand = new GetAllUserQuery
            {

            };
            var result = await Mediator.Send(getAllUserCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("[action]/{UserName}")]
        public async Task<IActionResult> Get([FromRoute] GetUserQuery getUserCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getUserCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] AddUserCommand userAddCommand,
            CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(userAddCommand, cancellationToken);
            if (result.IsSuccess)
            {
                string url = Url.Action(nameof(Get),
                    "User",
                    new { userName = userAddCommand.UserName },
                    Request.Scheme);

                return Created(url, result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateUserCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("[action]/{UserName}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserCommand deleteUserCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteUserCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
