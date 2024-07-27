using Application.Services;
using Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments;
using Application.Services.BankSafeTransactions.Command.AddBankSafeTransaction;
using Application.Services.BankSafeTransactions.Queries.GetAllBankSafeTransaction;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace WebSite.Controllers
{
    [Route("api/BankSafeTransactions")]
    [ApiController]
    public class BankSafeTransactionsController : BaseController
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllBankSafeTransactionQuery = new GetAllBankSafeTransactionQuery();
            var result = await Mediator.Send(getAllBankSafeTransactionQuery ,cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{Code}")]
        public async Task<IActionResult> Get([FromRoute] GetAllBankSafeTransactionQuery getAllBankSafeTransactionQuery,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getAllBankSafeTransactionQuery, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AddBankSafeTransactionCommand addBankSafeTransactionCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addBankSafeTransactionCommand, cancellationToken);
            if (result.IsSuccess)
            {
                string url = Url.Action(nameof(Get), "BankSafeTransactions",
                    new { Code = result.Data }, Request.Scheme);
                return Created(url,result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
