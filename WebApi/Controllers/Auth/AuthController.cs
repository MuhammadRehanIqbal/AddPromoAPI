using Application.Abstraction.Usecase.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static WebApi.Controllers.ApiBaseController;

namespace WebApi.Controllers.Auth
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ApiController
    { 
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> Login([FromBody] Login.Query query)
        {
            var result = await Mediator.Send(query);

            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }
    }
}
