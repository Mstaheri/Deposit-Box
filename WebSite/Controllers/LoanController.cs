using Application.Services.Loan.Commands.AddLoan;
using Application.Services.Loan.Commands.DeleteLoan;
using Application.Services.Loan.Queries.GetAllLoan;
using Application.Services.Loan.Queries.GetByCodeLoan;
using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/Loan")]
    [ApiController]
    public class LoanController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var getAllLoanQuery = new GetAllLoanQuery();
            var result = await Mediator.Send(getAllLoanQuery, cancellationToken);
            if (result.IsSuccess == true)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{Code}")]
        public async Task<IActionResult> GetByCode([FromRoute] GetByCodeLoanQuery getByCodeLoanQuery
            , CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(getByCodeLoanQuery, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpDelete("{UserName}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteLoanCommand deleteLoanCommand 
            ,CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(deleteLoanCommand , cancellationToken);
            if (result.IsSuccess == true)
            {
                return Ok(result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] AddLoanCommand addLoanCommand ,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(addLoanCommand , cancellationToken);
            if (result.IsSuccess == true)
            {
                string url = Url.Action(nameof(GetByCode), "loan",
                new
                {
                    Code = result.Data,
                    },
                    Request.Scheme);
                return Created(url, result.IsSuccess);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }
}
