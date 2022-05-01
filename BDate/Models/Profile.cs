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
        public Profile()
        {
            this.Personalities = new HashSet<Personality>();
            this.Hobbies = new HashSet<Hobby>();
        }

        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Setting Setting { get; set; }
        public virtual ICollection<Personality> Personalities { get; set; }
        public virtual ICollection<Hobby> Hobbies { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
