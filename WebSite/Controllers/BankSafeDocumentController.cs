using Application.Services;
using Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments;
using Application.Services.BankSafeDocuments.Queries.GetAllBankSafeDocuments;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankSafeDocumentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllBankSafeDocumentsQuery = new GetAllBankSafeDocumentsQuery();
            var result = await Mediator.Send(getAllBankSafeDocumentsQuery, cancellationToken);
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
        public async Task<IActionResult> Get([FromRoute] GetAllBankSafeDocumentsQuery getAllBankSafeDocumentsQuery
            , CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getAllBankSafeDocumentsQuery, cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] AddBankSafeDocumentsCommand addBankSafeDocumentsCommand,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addBankSafeDocumentsCommand, cancellationToken);
            if (result.IsSuccess)
            {
                string url = Url.Action(nameof(Get), "BankSafeDocument",
                    new { Code = result.Data }, Request.Scheme);
                return Created(url, result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
