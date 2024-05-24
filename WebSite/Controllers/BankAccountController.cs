using Application.Services;
using Application.Services.BankAccounts.Commands.AddBankAccount;
using Application.Services.BankAccounts.Commands.DeleteBankAccount;
using Application.Services.BankAccounts.Commands.UpdateBankAccount;
using Application.Services.BankAccounts.Queries.GetAllBankAccount;
using Application.Services.BankAccounts.Queries.GetBankAccount;
using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/BankAccount")]
    [ApiController]
    [Authorize]
    public class BankAccountController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllBankAccountQuery = new GetAllBankAccountQuery();
            var result = await Mediator.Send(getAllBankAccountQuery,cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{AccountNumber}")]
        public async Task<IActionResult> Get([FromRoute] GetBankAccountQuery getAllBankAccountQuery,
            CancellationToken cancellationToken)
        {
            
            var result = await Mediator.Send(getAllBankAccountQuery, cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] AddBankAccountCommand addBankAccountCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addBankAccountCommand, cancellationToken);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), 
                    "bankAccount", 
                    new { accountNumber = addBankAccountCommand.AccountNumber },
                    Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBankAccountCommand updateBankAccountCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateBankAccountCommand, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{AccountNumber}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteBankAccountCommand deleteBankAccountCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteBankAccountCommand, cancellationToken);
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
