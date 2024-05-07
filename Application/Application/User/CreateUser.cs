using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Application.ViewModels;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class CreateUser
    {
        public class Command : IRequest<UserVM>
        {
            public UserCreateVM User { get; set; }

            public Command(UserCreateVM user)
            {
                User = user;
            }
        }

        public class CommandValidator : AbstractValidator<UserCreateVM>
        {
            private readonly UserManager<AppUser> _userManager;

            public CommandValidator(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                RuleFor(x => x.UserName).NotEmpty()
                    .MustAsync(async (username, cancelation) => (await _userManager.FindByNameAsync(username) == null))
                    .WithMessage("Username already exist");
                RuleFor(x => x.Email).NotEmpty().EmailAddress()
                    .MustAsync(async (email, cancelation) => (await _userManager.FindByEmailAsync(email) == null))
                    .WithMessage("Email already exist");
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Password).Password();
                RuleFor(x => x.Avatar).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserVM>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJWTGenerator _JWTGenerator;

            public Handler(UserManager<AppUser> userManager, IJWTGenerator JWTGenerator)
            {
                _userManager = userManager;
                _JWTGenerator = JWTGenerator;
            }

            public async Task<UserVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser()
                {
                    Email = request.User.Email,
                    UserName = request.User.UserName,
                    Avatar = request.User.Avatar
                };

                var result = await _userManager.CreateAsync(user, request.User.Password);

                if (!result.Succeeded)
                    throw new ExceptionResponse(System.Net.HttpStatusCode.InternalServerError, new { message = "Error with user creation", result.Errors });

                var response = new UserVM
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _JWTGenerator.CreateToken(user),
                    Avatar = user.Avatar
                };

                return response;
            }
        }
    }
}
