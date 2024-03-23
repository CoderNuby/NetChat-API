using Application.ViewModels;
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
        public class Command : IRequest
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

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var channel = new Channel()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Channel.Name,
                    Description = request.Channel.Description
                };
                _context.Channels.Add(channel);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                    return Unit.Value;

                throw new Exception("An error was occurred");
            }
        }
    }
}
