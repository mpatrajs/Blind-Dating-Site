using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models {
    public class Role {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
