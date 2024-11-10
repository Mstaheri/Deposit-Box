using Application.Services;
using Application.Services.Users.Commands.AddUser;
using Application.Services.Users.Commands.DeleteUser;
using Application.Services.Users.Commands.UpdateUser;
using Application.Services.Users.Queries.GetAllUser;
using Application.Services.Users.Queries.GetByUserNameAndPassword;
using Application.Services.Users.Queries.GetUser;
using Domain.Entity;
using Domain.Exceptions;
using Glimpse.Core.ClientScript;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace WebSite.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly TokenService _tokenService;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> login([FromBody] GetByUserNameAndPasswordQuery getByUserNameAndPasswordQuery
            , CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(getByUserNameAndPasswordQuery, cancellationToken);
            if (result.IsSuccess == true)
            {
                if (result.Data != null)
                {
                    var token = _tokenService.GenerateToken(result.Data.UserName);
                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest("Sorry the credentials you are using are invalid");
                }
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize]
        [HttpGet("{UserName}")]
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

        [AllowAnonymous]
        [HttpPost]
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

        [Authorize]
        [HttpPut]
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

        [Authorize]
        [HttpDelete("{UserName}")]
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
