using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class GetUsers
    {
        public class Query : IRequest<List<UserVM>> { }

        public class Handler : IRequestHandler<Query, List<UserVM>>
        {
            private DataContext _context;
            private IMapper _mapper;

            public Handler(DataContext context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<UserVM>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.ToListAsync();

                var response = _mapper.Map<List<UserVM>>(users);

                return response;
            }
        }
    }
}
