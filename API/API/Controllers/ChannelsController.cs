﻿using Application.Channels;
using Application.ViewModels;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ChannelsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelVM>> Get(Guid id)
        {
            return await _mediator.Send(new GetChannelById.Query(id));
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<ChannelVM>>> GetAll()
        {
            return await _mediator.Send(new GetChannels.Query());
        }

        [HttpPost]
        public async Task<ActionResult<ChannelVM>> Create([FromBody] ChannelCreateVM channel)
        {
            return await _mediator.Send(new CreateChannel.Command(channel));
        }

        [HttpPost("private/{channelId}")]
        public async Task<ActionResult<ChannelVM>> CreatePrivateDetails(string channelId)
        {
            return await _mediator.Send(new CreatePrivateChannelDetails.Command(channelId));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChannelVM>> Update(Guid id, [FromBody] ChannelUpdateVM channel)
        {
            channel.Id = id;
            return await _mediator.Send(new UpdateChannel.Command(channel));
        }
    }
}
