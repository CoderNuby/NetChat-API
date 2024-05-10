using Application.Messages;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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
            var message = _mediator.Send(new CreateMessage.Command(messageVm));

            await Clients.All.SendAsync("ReceiveMessage", message);

            var response = message.Result;
            return response;
        }
    }
}
