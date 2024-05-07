using Application.ViewModels;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Channels
{
    public class CreateChannel
    {
        public class Command : IRequest<ChannelVM>
        {
            public ChannelCreateVM Channel { get; set; }
            public Command(ChannelCreateVM channel) 
            {
                Channel = channel;
            }
        }

        public class CommandValidator : AbstractValidator<ChannelCreateVM>
        {
            public CommandValidator() 
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ChannelVM>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            public async Task<ChannelVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var channel = new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Channel.Name,
                    Description = request.Channel.Description,
                    ChannelType = ChannelTypeEnum.Channel
                };
                _context.Channels.Add(channel);
                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    var channelDB = await _context.Channels.FindAsync(channel.Id);
                    var response = _mapper.Map<ChannelVM>(channelDB);
                    return response;
                }

                throw new Exception("An error was occurred");
            }
        }
    }
}
