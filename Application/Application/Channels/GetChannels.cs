using Application.ViewModels;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Channels
{
    public class GetChannels
    {
        public class Query : IRequest<List<ChannelVM>> 
        {
        }

        public class Handler : IRequestHandler<Query, List<ChannelVM>>
        {
            private readonly DataContext _dataContext;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, ILogger<GetChannels> logger, IMapper mapper)
            {
                _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            }
            public async Task<List<ChannelVM>> Handle(Query request, CancellationToken cancellationToken)
            {
                IEnumerable<Channel> channels = await _dataContext.Channels
                    .Include(x => x.Messages).ThenInclude(x => x.Sender).ToListAsync();
                var response = _mapper.Map<IEnumerable<Channel>, IEnumerable <ChannelVM>>(channels);
                return response.ToList();
            }
        }
    }
}
