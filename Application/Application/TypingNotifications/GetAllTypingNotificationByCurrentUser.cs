using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TypingNotifications
{
    public class GetAllTypingNotificationByCurrentUser
    {
        public class Query : IRequest<List<TypingNotificationVM>> { }

        public class Handler : IRequestHandler<Query, List<TypingNotificationVM>>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccess userAccess, IMapper mapper) 
            {
                _userAccess = userAccess;
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<TypingNotificationVM>> Handle(Query request, CancellationToken cancellationToken)
            {
                var typingNotifications = await _context.TypingNotifications.Include(x => x.Channel).Include(x => x.Sender).Where(x => x.Sender.UserName == _userAccess.GetCurrentUserName()).ToListAsync();

                var response = _mapper.Map<List<TypingNotificationVM>>(typingNotifications);

                return response;
            }
        }
    }
}
