using Application.Channels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Domain;
using Application.User;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseVM>> Login([FromBody] UserLoginVM user)
        {
            return await _mediator.Send(new Login.Query(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseVM>> Register([FromBody] UserCreateVM user)
        {
            return await _mediator.Send(new CreateUser.Command(user));
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserResponseVM>> CurrentUser()
        {
            return await _mediator.Send(new GetCurrentUser.Query());
        }
    }
}
