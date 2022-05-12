using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models {
    public class Setting {
        [ForeignKey("Profile")]
        public string SettingId { get; set; }
        [Display(Name = "Hide age")]
        public bool isHiddenAge { get; set; }
        [Display(Name = "Hide Last name")]
        public bool isHiddenLastName { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
