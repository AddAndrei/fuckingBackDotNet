using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PhoneRu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator =>
            _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
