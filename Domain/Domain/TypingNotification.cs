using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TypingNotification
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
    }
}
