using Application.Codes.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneRu.Atributes;

namespace PhoneRu.Controllers
{
    public class VerificationCodeController : BaseController
    {
        /// <summary>
        ///  Создать код
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Unit>> CreateCode(CreateVerificationCodeCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        ///  Проверить код
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("Verificate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Unit>> VerificateCode(VerificateVerificationCodeCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        ///  Создать код
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("test")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Unit>> Test(CreateVerificationCodeCommand command)
        {
            return Unit.Value;
        }
    }
}
