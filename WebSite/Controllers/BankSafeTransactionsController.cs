using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace WebSite.Controllers
{
    [Route("api/BankSafeTransactions")]
    [ApiController]
    public class BankSafeTransactionsController : ControllerBase
    {
        private readonly BankSafeTransactionsService _bankSafeTransactionsService;
        public BankSafeTransactionsController(BankSafeTransactionsService bankSafeTransactionsService)
        {
            _bankSafeTransactionsService = bankSafeTransactionsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankSafeTransactionsService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{Code}")]
        public async Task<IActionResult> Get([FromRoute] Guid Code)
        {

            var result = await _bankSafeTransactionsService.GetAsync(Code);
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
        public async Task<IActionResult> Insert([FromBody] BankSafeTransactions bankSafeTransactions)
        {
            var result = await _bankSafeTransactionsService.AddAsync(bankSafeTransactions);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), "BankSafeTransactions",
                    new { Code = bankSafeTransactions.Code }, Request.Scheme);
                return Created(url,result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
