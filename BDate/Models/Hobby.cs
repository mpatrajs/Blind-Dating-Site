using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models
{
    public class Hobby
    {
        public Hobby()
        {
            this.Profiles = new HashSet<Profile>();
        }
        public string HobbyId { get; set; }
        [Display(Name = "Hobby name")]
        public string HobbyName { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
