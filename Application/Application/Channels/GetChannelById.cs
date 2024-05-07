using Application.Errors;
using Application.ViewModels;
using AutoMapper;
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
        public class Query : IRequest<ChannelVM>
        {
            public Guid Id { get; set; }
            public Query(Guid id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, ChannelVM>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public async Task<ChannelVM> Handle(Query request, CancellationToken cancellationToken)
            {
                var channel = await _dataContext.Channels.Include(x => x.Messages)
                    .ThenInclude(x => x.Sender).FirstOrDefaultAsync(x => x.Id == request.Id);
                if (channel == null)
                    throw new ExceptionResponse(HttpStatusCode.NotFound, new { channel = "Not found"});
                
                var response = _mapper.Map<ChannelVM>(channel);
                
                return response;
            }
        }
    }
}
