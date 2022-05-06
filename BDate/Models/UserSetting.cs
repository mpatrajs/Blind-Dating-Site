using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDate.Models
{
    public class UserSetting
    {
        [Key]
        public long ID { get; set; }
        [Display(Name = "Setting name")]
        public string SettingName { get; set; }
        public Boolean SettingOn { get; set; }
    }
}
