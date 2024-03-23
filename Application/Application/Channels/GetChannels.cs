using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Channels
{
    public class GetChannels
    {
        public class Query : IRequest<List<Channel>> 
        {
        }

        public class Handler : IRequestHandler<Query, List<Channel>>
        {
            private readonly DataContext _dataContext;
            private readonly ILogger _logger;

            public Handler(DataContext dataContext, ILogger<GetChannels> logger)
            {
                _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }
            public async Task<List<Channel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _dataContext.Channels.ToListAsync();
            }
        }
    }
}
