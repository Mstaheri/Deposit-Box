using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
