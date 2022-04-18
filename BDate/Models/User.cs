using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDate.Models
{
    public class User
    {
        [Key]
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AmericanGender { get; set; }
        public string Personal { get; set; }
        public string Hobbies { get; set; }
    }
}
