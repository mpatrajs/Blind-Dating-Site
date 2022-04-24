using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDate.Models
{
    public class Profile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public bool isActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Personal { get; set; }
        public string Hobbies { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
