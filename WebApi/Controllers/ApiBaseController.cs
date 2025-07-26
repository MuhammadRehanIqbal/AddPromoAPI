using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ApiBaseController
    {
        [Route("api/[controller]")]
        [ApiController]
        public abstract class ApiController : ControllerBase
        {

            //Declaring IMediator in an abstract class allows all controllers that inherit from this 
            //base class to share the same MediatR functionality without repeating the code.

            private IMediator _mediator;
            protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }
    }
}
