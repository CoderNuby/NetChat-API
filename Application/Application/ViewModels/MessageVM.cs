using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class MessageVM
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public UserVM Sender { get; set; }
        public MessageTypeEnum MessageType { get; set; }
    }
}
