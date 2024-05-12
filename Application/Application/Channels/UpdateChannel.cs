using Application.Errors;
using Application.ViewModels;
using AutoMapper;
using MediatR;
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
    public class UpdateChannel
    {
        public class Command : IRequest<ChannelVM>
        {
            public ChannelUpdateVM Channel { get; set; }

            public Command(ChannelUpdateVM channel) 
            {
                Channel = channel;
            }
        }

        public class Handler : IRequestHandler<Command, ChannelVM>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ChannelVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var channel = await _context.Channels.FindAsync(request.Channel.Id);

                if(channel == null)
                    throw new ExceptionResponse(HttpStatusCode.NotFound, new { channel = "Not found" });

                channel.ChannelType = request.Channel.ChannelType;
                channel.Name = request.Channel.Name ?? channel.Name;
                channel.Description = request.Channel.Description ?? channel.Description;

                var success = await _context.SaveChangesAsync() > 0;

                if(success)
                {
                    return _mapper.Map<ChannelVM>(channel);
                }

                throw new ExceptionResponse(HttpStatusCode.InternalServerError, new { message = "Error" });
            }
        }
    }
}
