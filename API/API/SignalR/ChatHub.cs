using Application.Messages;
using Application.TypingNotifications;
using Application.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<MessageVM> SendMessage(MessageCreateVM messageVm)
        {
            var message = await _mediator.Send(new CreateMessage.Command(messageVm));

            await Clients.All.SendAsync("ReceiveMessage", message);

            var response = message;
            return response;
        }

        public async Task<TypingNotificationVM> SendTypingNotification(Guid channelId)
        {
            var notification = await _mediator.Send(new CreateTypingNotification.Command(channelId));

            await Clients.All.SendAsync("ReceiveTypingNotification", notification);

            var response = notification;
            return response;
        }

        public async Task<TypingNotificationVM> DeleteTypingNotification(Guid typingId)
        {
            var notification = await _mediator.Send(new DeleteTypingNotification.Command(typingId));

            await Clients.All.SendAsync("ReceiveDeleteTypingNotification", notification);

            var response = notification;
            return response;
        }
    }
}
