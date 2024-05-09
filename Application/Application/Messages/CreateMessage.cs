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

namespace Application.Messages
{
    public class CreateMessage
    {
        public class Command: IRequest<MessageVM>
        {
            public MessageCreateVM Message { get; set; }

            public Command(MessageCreateVM message) 
            {
                Message = message;
            }
        }

        public class Handler : IRequestHandler<Command, MessageVM>
        {
            public DataContext _context;
            public IUserAccess _userAccess;
            public IMapper _mapper;
            public IMediaUpload _mediaUpload;
            public Handler(DataContext context,
                IUserAccess userAccesor,
                IMapper mapper,
                IMediaUpload mediaUpload)
            {
                _context = context;
                _userAccess = userAccesor;
                _mapper = mapper;
                _mediaUpload = mediaUpload;
            }

            public async Task<MessageVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccess.GetCurrentUserName());

                var channel = await _context.Channels.SingleOrDefaultAsync(x => x.Id == request.Message.ChannelId);

                if (channel == null)
                    throw new ExceptionResponse(System.Net.HttpStatusCode.NotFound, new { channel = "Channel not found" });

                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    Content = request.Message.MessageType == MessageTypeEnum.Text ? request.Message.Content : _mediaUpload.UploadMedia(request.Message.File).Url,
                    Channel = channel,
                    Sender = user,
                    CreatedAt = DateTime.Now,
                    MessageType = request.Message.MessageType
                };

                _context.Messages.Add(message);

                if(await _context.SaveChangesAsync() > 0)
                {

                    var messageDB = await _context.Messages.Include(x => x.Sender).FirstOrDefaultAsync(x => x.Id == message.Id);

                    if (messageDB == null)
                        throw new ExceptionResponse(System.Net.HttpStatusCode.NotFound, new { message = "Message not found" });

                    var response = _mapper.Map<MessageVM>(messageDB);

                    return response;
                }

                throw new ExceptionResponse(System.Net.HttpStatusCode.InternalServerError, new { message = "Internal error" });
            }
        }
    }
}
