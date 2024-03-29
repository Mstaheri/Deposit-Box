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
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankSafeDocumentService.GetAllAsync();
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

            var result = await _bankSafeDocumentService.GetAsync(Code);
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
        public async Task<IActionResult> Insert([FromBody] BankSafeDocument bankSafeDocument)
        {
            var result = await _bankSafeDocumentService.AddAsync(bankSafeDocument);
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
