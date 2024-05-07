using Application.Messages;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MessagesController : BaseController
    {
        [HttpPost]
        public Task<MessageVM> Create([FromBody] MessageCreateVM message)
        {
            return _mediator.Send(new CreateMessage.Command(message));
        }
    }
}
