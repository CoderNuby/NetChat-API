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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Channels
{
    public class CreatePrivateChannelDetails
    {
        public class Command : IRequest<ChannelVM> 
        {
            public string UserId { get; set; }

            public Command(string userId) 
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Command, ChannelVM>
        {
            private DataContext _context;
            private IMapper _mapper;
            private IUserAccess _userAccess;

            public Handler(DataContext context, IMapper mapper, IUserAccess userAccess)
            {
                _context = context;
                _mapper = mapper;
                _userAccess = userAccess;
            }

            public async Task<ChannelVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccess.GetCurrentUserName());

                var user = await _context.Users.FindAsync(request.UserId);

                var privateChannelIdForCurrentUser = GetPrivateChannelId(currentUser.Id.ToString(), request.UserId);
                var privateChannelIdForRecipientUser = GetPrivateChannelId(request.UserId, currentUser.Id.ToString());

                var channel = await _context.Channels.Include(x => x.Messages).ThenInclude(x => x.Sender)
                    .SingleOrDefaultAsync(x => x.PrivateChannelId == privateChannelIdForCurrentUser
                                            || x.PrivateChannelId == privateChannelIdForRecipientUser);

                if (channel == null)
                {
                    var newChannel = new Channel
                    {
                        Id = Guid.NewGuid(),
                        Name = currentUser.UserName,
                        Description = user.UserName,
                        ChannelType = ChannelTypeEnum.Room,
                        PrivateChannelId = privateChannelIdForCurrentUser
                    };

                    var channelCreated = _context.Channels.Add(newChannel);

                    var success = await _context.SaveChangesAsync() > 0;
                    if (success)
                    {
                        return _mapper.Map<ChannelVM>(newChannel);
                    }

                    throw new ExceptionResponse(HttpStatusCode.InternalServerError, new { channel = "Error server" });
                }

                return _mapper.Map<ChannelVM>(channel);
            }

            private string GetPrivateChannelId(string currentId, string userId)
            {
                return $"{currentId}-{userId}";
            }
        }
    }
}
