using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class TypingNotificationVM
    {
        public Guid Id { get; set; }
        public UserVM Sender { get; set; }
        public ChannelVM Channel { get; set; }
    }
}
