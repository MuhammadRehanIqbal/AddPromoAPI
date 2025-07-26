using Application.Abstraction.Auth.Interfaces;
using Application.Abstraction.Auth.Services;
using Application.Abstraction.Persistence.Users;
using FluentValidation;
using MediatR; 

namespace Application.Abstraction.Usecase.Auth
{
    public class Login
    { 
        public class Query : IRequest<Response>
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
          
        public class Response
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string Role { get; set; }
        } 
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            }
        }
         
        public class Handler : IRequestHandler<Query, Response?>
        {
            private readonly IUserRepository _userRepository;
            private readonly IJwtTokenService _jwtTokenService;
            private readonly IRefreshTokenRepository _refreshTokenRepository;

            public Handler(
                IUserRepository userRepository,
                IJwtTokenService jwtTokenService,
                IRefreshTokenRepository refreshTokenRepository)
            {
                _userRepository = userRepository;
                _jwtTokenService = jwtTokenService;
                _refreshTokenRepository = refreshTokenRepository;
            }

            public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.ValidateUserAsync(request.Username, request.Password);
                if (user == null) return null;

                var token = _jwtTokenService.GenerateToken(user);
               // var refreshToken = await _refreshTokenRepository.GenerateToken(request.Username);

                return new Response
                {
                    Token = token,
                    //RefreshToken = refreshToken,
                    RefreshToken = token,
                    Role = user.Role
                };
            }
        }
    }
}
