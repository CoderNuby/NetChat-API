using API.SignalR;
using Application.Messages;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(IHubContext<ChatHub> hubContext) 
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public Task<MessageVM> Create([FromBody] MessageCreateVM message)
        {
            return _mediator.Send(new CreateMessage.Command(message));
        }

        [HttpPost("upload")]
        public async Task<MessageVM> MediaUpload([FromForm] MessageCreateVM message)
        {
            var response = _mediator.Send(new CreateMessage.Command(message));

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", response);
            return response.Result;
        }
    }
}
