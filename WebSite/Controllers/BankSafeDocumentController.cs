using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/BankSafeDocument")]
    [ApiController]
    public class BankSafeDocumentController : ControllerBase
    {
        private readonly BankSafeDocumentService _bankSafeDocumentService;
        public BankSafeDocumentController(BankSafeDocumentService bankSafeDocumentService)
        {
            _bankSafeDocumentService = bankSafeDocumentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocumentService.GetAllAsync(cancellationToken);
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
        public async Task<IActionResult> Get([FromRoute] Guid Code
            , CancellationToken cancellationToken)
        {

            var result = await _bankSafeDocumentService.GetAsync(Code , cancellationToken);
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
        public async Task<IActionResult> Insert([FromBody] BankSafeDocument bankSafeDocument,
            CancellationToken cancellationToken)
        {
            var result = await _bankSafeDocumentService.AddAsync(bankSafeDocument , cancellationToken);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), "BankSafeDocument",
                    new { Code = bankSafeDocument.Code }, Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
