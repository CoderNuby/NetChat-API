using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class MessageCreateVM
    {
        public string Content { get; set; }
        public Guid ChannelId { get; set; }
        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.Text;
    }
}
