using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Channels
{
    public class GetChannelById
    {
        public class Query : IRequest<Channel>
        {
            public Guid Id { get; set; }
            public Query(Guid id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Channel>
        {
            private readonly DataContext _dataContext;

            public Handler(DataContext dataContext)
            {
                _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            }
            public async Task<Channel> Handle(Query request, CancellationToken cancellationToken)
            {
                var channel = await _dataContext.Channels.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (channel == null)
                    throw new ExceptionResponse(HttpStatusCode.NotFound, new { channel = "Not found"});
                return channel;
            }
        }
    }
}
