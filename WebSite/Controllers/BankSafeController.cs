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
    [Route("api/BankSafe")]
    [ApiController]
    public class BankSafeController : BaseController
    {
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllBankSafeQuery = new GetAllBankSafeQuery();
            var result = await Mediator.Send(getAllBankSafeQuery,cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [Route("inventory")]
        [HttpGet]
        public async Task<IActionResult> Inventory(CancellationToken cancellationToken)
        {
            var inventoryBankSafeQuery = new InventoryBankSafeQuery();
            var result = await Mediator.Send(inventoryBankSafeQuery,cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{Name}")]
        public async Task<IActionResult> Get([FromRoute] GetBankSafeQuery getBankSafeQuery,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getBankSafeQuery, cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] AddBankSafeCommand addBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addBankSafeCommand, cancellationToken);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get),
                    "BankSafe", 
                    new { name = addBankSafeCommand.Name },
                    Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBankSafeCommand updateBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(updateBankSafeCommand, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{Name}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteBankSafeCommand deleteBankSafeCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteBankSafeCommand, cancellationToken);
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
