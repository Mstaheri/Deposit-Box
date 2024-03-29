using Application.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Route("api/BankSafe")]
    [ApiController]
    public class BankSafeController : ControllerBase
    {
        private readonly BankSafeService _bankSafeService;
        public BankSafeController(BankSafeService bankSafeService)
        {
            _bankSafeService = bankSafeService;
        }
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bankSafeService.GetAllAsync();
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
        public async Task<IActionResult> Inventory()
        {
            var result = await _bankSafeService.Inventory();
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
        public async Task<IActionResult> Get([FromRoute] string Name)
        {
            var result = await _bankSafeService.GetAsync(Name);
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
        public async Task<IActionResult> Insert([FromBody] BankSafe bankSafe)
        {
            var result = await _bankSafeService.AddAsync(bankSafe);
            if (result.Success)
            {
                string url = Url.Action(nameof(Get), "BankSafe", new { name = bankSafe.Name.Value }, Request.Scheme);
                return Created(url, result.Success);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BankSafe bankSafe)
        {
            var result = await _bankSafeService.UpdateAsync(bankSafe);
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
        public async Task<IActionResult> Delete([FromRoute] string Name)
        {
            var result = await _bankSafeService.DeleteAsync(Name);
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
