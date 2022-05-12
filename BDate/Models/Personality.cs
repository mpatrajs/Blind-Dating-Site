using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models {
    public class Personality {
        public Personality() {
            this.Profiles = new HashSet<Profile>();
        }
        public string PersonalityId { get; set; }
        [Display(Name = "Personality name")]
        public string PersonalityName { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
