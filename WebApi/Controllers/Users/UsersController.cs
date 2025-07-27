using Application.Abstraction.Usecase.Auth;
using Application.Abstraction.Usecase.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using static WebApi.Controllers.ApiBaseController;

namespace WebApi.Controllers.Users
{

    [Route("api/users")]
    [ApiController]
    [Authorize] // Require a valid token
    public class UsersController : ApiController
    {  
        [HttpGet("get_menu_by_user")]
        public async Task<IActionResult> GetMenuByUser()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value; 
            
            if (string.IsNullOrEmpty(role))
                return Unauthorized("Role not found in token.");


            var query = new GetMenusByrole.Query { Role = role }; 
            var result = await Mediator.Send(query); 
            return Ok(result);
        }
    }
}
