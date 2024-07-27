using Application.Services;
using Application.Services.BankSafes.Commands.AddBankSafe;
using Application.Services.BankSafes.Commands.DeleteBankSafe;
using Application.Services.BankSafes.Commands.UpdateBankSafe;
using Application.Services.BankSafes.Queries.GetAllBankSafe;
using Application.Services.BankSafes.Queries.GetBankSafe;
using Application.Services.BankSafes.Queries.InventoryBankSafe;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankSafeController : BaseController
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllBankSafeQuery = new GetAllBankSafeQuery();
            var result = await Mediator.Send(getAllBankSafeQuery,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Inventory(CancellationToken cancellationToken)
        {
            var inventoryBankSafeQuery = new InventoryBankSafeQuery();
            var result = await Mediator.Send(inventoryBankSafeQuery,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> Get([FromRoute] GetBankSafeQuery getBankSafeQuery,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getBankSafeQuery, cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] AddBankSafeCommand addBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addBankSafeCommand, cancellationToken);
            if (result.IsSuccess)
            {
                string url = Url.Action(nameof(Get),
                    "BankSafe", 
                    new { name = addBankSafeCommand.Name },
                    Request.Scheme);
                return Created(url, result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateBankSafeCommand updateBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateBankSafeCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("[action]/{Name}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteBankSafeCommand deleteBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteBankSafeCommand, cancellationToken);
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
