using Application.Abstraction.Persistence.Users;
using Core.Entities;
using FluentValidation;
using MediatR; 

namespace Application.Abstraction.Usecase.Users
{
public class GetMenusByrole
{ 
        public class Query : IRequest<Response>
        {
            public required string Role { get; set; } 
        }
        
        public class Response
        {
            public List<MenuItem> Menus { get; set; } = new List<MenuItem>();
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required"); 
            }
        }

        public class Handler : IRequestHandler<Query, Response?>
        {
            private readonly IUserRepository _userRepository; 

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
            {
                var menus = await _userRepository.GetMenusByRoleAsync(request.Role);
                if (menus == null) return null;


                return new Response
                {
                    Menus = menus.ToList()
                };
            }
        }
    }
}
