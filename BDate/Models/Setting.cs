using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models
{
    public class Setting
    {
        [ForeignKey("Profile")]
        public string SettingId { get; set; }
        public bool isHiddenAge { get; set; }
        public bool isHiddenLastName { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
