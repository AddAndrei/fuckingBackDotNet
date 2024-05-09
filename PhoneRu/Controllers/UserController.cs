using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserToken;
using Microsoft.AspNetCore.Mvc;

namespace PhoneRu.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        ///     Создать пользователя
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateUser(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        ///     Получить токен
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost("Authorize")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> GetToken(GetUserTokenQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
