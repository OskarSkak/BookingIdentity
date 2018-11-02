using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Platibus.Identity.CreateUserModels;
using Platibus.Identity.Handlers;

namespace Platibus.Identity.Controllers
{
    [Route("identity/users")]
    public class CreateUserController : Controller
    {
        private readonly ICreateUserHandler _userHandler;

        public CreateUserController(ICreateUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequestModel requestModel)
        {
            var result =  await _userHandler.CreateUser(requestModel);

            if (!result.IsSuccessful)
            {
                return StatusCode(400, result.Message);
            }

            return StatusCode(200);
        }
    }
}