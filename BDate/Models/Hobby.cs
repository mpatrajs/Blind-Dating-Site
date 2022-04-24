using System;
using System.Collections.Generic;
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
        public string HobbyName { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
