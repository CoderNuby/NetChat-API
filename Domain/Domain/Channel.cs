using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Channel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ChannelTypeEnum ChannelType { get; set; }
        public string PrivateChannelId { get; set; }
        [JsonIgnore]
        public ICollection<TypingNotification> TypingNotifications { get; set; }
    }
}
