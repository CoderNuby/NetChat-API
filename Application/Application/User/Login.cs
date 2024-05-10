using Application.Errors;
using Application.Interfaces;
using Application.ViewModels;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Login
    {
        public class Query : IRequest<UserVM>
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

        public class Handler : IRequestHandler<Query, UserVM>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJWTGenerator _jwtGenerator;
            private readonly DataContext _context;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJWTGenerator jwtGenerator, DataContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
            }

            public async Task<UserVM> Handle(Query request, CancellationToken cancellationToken)
            {
                var userDB = await _userManager.FindByEmailAsync(request.User.Email);

                if (userDB == null)
                {
                    throw new ExceptionResponse(HttpStatusCode.Unauthorized);
                }

                var auth = await _signInManager.CheckPasswordSignInAsync(userDB, request.User.Password, false);

                if (auth.Succeeded)
                {
                    userDB.IsOnline = true;
                    await _context.SaveChangesAsync();
                    var user = new UserVM() 
                    {
                        Id = userDB.Id,
                        Token = _jwtGenerator.CreateToken(userDB),
                        UserName = userDB.UserName,
                        Email = userDB.Email,
                        Avatar = userDB.Avatar,
                        IsOnline = userDB.IsOnline
                    };

                    return user;
                }

                throw new ExceptionResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}
