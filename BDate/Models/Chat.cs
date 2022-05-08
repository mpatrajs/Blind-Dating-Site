using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models
{
    public class Chat
    {
        [Key, ForeignKey("Profile")]
        [InverseProperty("Chats")]
        public string fromProfileId { get; set; }
        [Key]
        [InverseProperty("Chats")]
        public string toProfileId { get; set; }
        public string roomId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
