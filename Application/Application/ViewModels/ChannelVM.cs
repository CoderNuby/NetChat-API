using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ChannelVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MessageVM> Messages { get; set; }
        public ChannelTypeEnum ChannelType { get; set; }
        public string PrivateChannelId { get; set; }
    }
}
