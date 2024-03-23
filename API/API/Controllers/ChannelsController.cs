using Application.Channels;
using Application.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelsController : ControllerBase
    {
        private IMediator _mediator;

        public ChannelsController(IMediator mediator) 
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> Get(Guid id)
        {
            return await _mediator.Send(new GetChannelById.Query(id));
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<Channel>>> GetAll()
        {
            return await _mediator.Send(new GetChannels.Query());
        }

        [HttpPost]
        public async Task<Unit> Create([FromBody] ChannelCreateVM channel)
        {
            return await _mediator.Send(new CreateChannel.Command(channel));
        }
    }
}
