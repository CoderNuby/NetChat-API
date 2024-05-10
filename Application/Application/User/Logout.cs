using Application.Errors;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
    public class Logout
    {
        public class Query : IRequest 
        {
            public string UserId { get; set; }

            public Query(string userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.UserId);

                if(user == null)
                    throw new ExceptionResponse(HttpStatusCode.NotFound);

                if (!user.IsOnline)
                    return Unit.Value;

                user.IsOnline = false;

                var success = await _context.SaveChangesAsync() > 0;

                if(success)
                    return Unit.Value;

                throw new ExceptionResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
