using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
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
        public class Query : IRequest<UserVM>
        {

        }

        public class Handler : IRequestHandler<Query, UserVM>
        {
            private readonly IUserAccess _userAccess;
            private readonly UserManager<AppUser> _userManager;
            private readonly IJWTGenerator _JWTGenerator;
            private readonly IMapper _mapper;

            public Handler(IUserAccess userAccess, UserManager<AppUser> userManager, IJWTGenerator jWTGenerator, IMapper mapper)
            {
                _userAccess = userAccess;
                _userManager = userManager;
                _JWTGenerator = jWTGenerator;
                _mapper = mapper;
            }

            public async Task<UserVM> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userAccess.GetCurrentUserName());

                var response = _mapper.Map<UserVM>(user);

                response.Token = _JWTGenerator.CreateToken(user);

                return response;
            }
        }
    }
}
