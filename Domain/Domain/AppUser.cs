using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Avatar {  get; set; }
        [JsonIgnore]
        public ICollection<Message> Messages { get; set; }
        public bool IsOnline { get; set; }
    }
}
