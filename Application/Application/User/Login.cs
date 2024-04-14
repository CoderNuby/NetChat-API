using Application.Errors;
using Application.Interfaces;
using Application.ViewModels;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Login
    {
        public class Query : IRequest<UserResponseVM>
        {
            public UserLoginVM User;

            public Query(UserLoginVM user)
            {
                User = user;
            }
        }

        public class CommandValidator : AbstractValidator<UserLoginVM>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserResponseVM>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJWTGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserResponseVM> Handle(Query request, CancellationToken cancellationToken)
            {
                var userDB = await _userManager.FindByEmailAsync(request.User.Email);

                if (userDB == null)
                {
                    throw new ExceptionResponse(HttpStatusCode.Unauthorized);
                }

                var auth = await _signInManager.CheckPasswordSignInAsync(userDB, request.User.Password, false);

                if (auth.Succeeded)
                {
                    var user = new UserResponseVM() 
                    {
                        Token = _jwtGenerator.CreateToken(userDB),
                        UserName = userDB.UserName,
                        Email = userDB.Email
                    };

                    return user;
                }

                throw new ExceptionResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
