using Application.Errors;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TypingNotifications
{
    public class CreateTypingNotification
    {
        public class Command : IRequest<TypingNotificationVM>
        {
            public Guid ChannelId { get; set; }

            public Command(Guid channelId)
            {
                ChannelId = channelId;
            }
        }

        public class Handler : IRequestHandler<Command, TypingNotificationVM>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccess userAccess, IMapper mapper)
            {
                _context = context;
                _userAccess = userAccess;
                _mapper = mapper;
            }

            public async Task<TypingNotificationVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccess.GetCurrentUserName());

                var channel = await _context.Channels.SingleOrDefaultAsync(x => x.Id == request.ChannelId);

                if (channel == null)
                    throw new ResponseException(System.Net.HttpStatusCode.NotFound, new { message = "Channel not found"});

                var typing = new TypingNotification
                {
                    Id = Guid.NewGuid(),
                    ChannelId = channel.Id,
                    Channel = channel,
                    SenderId = user.Id,
                    Sender = user
                };

                await _context.TypingNotifications.AddAsync(typing);

                if (await _context.SaveChangesAsync() > 0)
                {
                    var response = _mapper.Map<TypingNotificationVM>(typing);
                    return response;
                }

                throw new ResponseException(System.Net.HttpStatusCode.InternalServerError, new { message = "Internal error server" });
            }
        }
    }
}
