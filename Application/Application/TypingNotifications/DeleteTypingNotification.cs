using Application.Errors;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TypingNotifications
{
    public class DeleteTypingNotification
    {
        public class Command : IRequest<TypingNotificationVM> 
        {
            public Guid TypingId { get; set; }

            public Command(Guid typingId)
            {
                TypingId = typingId;
            }
        }

        public class Handler : IRequestHandler<Command, TypingNotificationVM>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IMapper mapper, IUserAccess userAccess)
            {
                _context = context;
                _mapper = mapper;
                _userAccess = userAccess;
            }

            public async Task<TypingNotificationVM> Handle(Command request, CancellationToken cancellationToken)
            {
                var typing = await _context.TypingNotifications.Include(x => x.Sender).Include(x => x.Channel).SingleOrDefaultAsync(x => x.Id == request.TypingId);

                if (typing == null)
                    return null;

                _context.Remove(typing);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return _mapper.Map<TypingNotificationVM>(typing);
                }

                throw new ResponseException(System.Net.HttpStatusCode.InternalServerError, new { message = "Internal error server" });
            }
        }
    }
}
