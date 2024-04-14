using Application.Interfaces;
using Application.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class GetCurrentUser
    {
        public class Query : IRequest<UserResponseVM>
        {

        }

        public class Handler : IRequestHandler<Query, UserResponseVM>
        {
            private readonly IUserAccess _userAccess;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJWTGenerator _JWTGenerator;

            public Handler(IUserAccess userAccess, UserManager<AppUser> userManager, IJWTGenerator jWTGenerator)
            {
                _userAccess = userAccess;
                _userManager = userManager;
                _JWTGenerator = jWTGenerator;
            }

            public async Task<UserResponseVM> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccess.GetCurrentUserName());

                var response = new UserResponseVM
                {
                    UserName = user.UserName,
                    Token = _JWTGenerator.CreateToken(user)
                };

                return response;
            }
        }
    }
}
