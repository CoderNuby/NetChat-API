using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser Sender { get; set; }
        public string SenderId { get; set; }
        [JsonIgnore]
        public Channel Channel { get; set; }
        public Guid ChannelId { get; set; }
        public MessageTypeEnum MessageType { get; set; }
    }
}
