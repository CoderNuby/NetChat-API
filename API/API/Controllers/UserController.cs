using Application.Messages;
using Application.User;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UserController : BaseController
    {

        [HttpGet("All")]
        public Task<List<UserVM>> GetAll()
        {
            return _mediator.Send(new GetUsers.Query());
        }
    }
}
