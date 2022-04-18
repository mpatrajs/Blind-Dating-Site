using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDate.Models
{
    public class Test
    {
        [Key]
        public long ID { get; set; }
        public string FirstName { get; set; }
    }
}
