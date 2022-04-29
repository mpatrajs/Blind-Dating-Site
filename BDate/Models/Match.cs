using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models
{
    public class Match
    {
        [Key, ForeignKey("Profile")]
        [InverseProperty("Matches")]
        public string fromProfileId { get; set; }
        [Key]
        [InverseProperty("Matches")]
        public string toProfileId { get; set; }
        //[ForeignKey("fromProfileId,toProfileId")]
        public virtual Profile Profile { get; set; }
    }
}
