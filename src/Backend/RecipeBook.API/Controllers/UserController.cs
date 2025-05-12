using Microsoft.AspNetCore.Mvc;
using RecipeBook.Application.UseCases.User.Interfaces;
using RecipeBook.Comunication.Requests;
using RecipeBook.Comunication.Responses;

namespace RecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUser user,
            [FromBody] RequestRegisterUserJson request
            )
        {
            var result = await user.Execute(request);
            return Created(string.Empty, result);
        }

    }
}
