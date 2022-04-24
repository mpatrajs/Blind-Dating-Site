using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDate.Models
{
    public class UserToCategories
    {
        [Key]
        public long Relation_ID { get; set; }
        public long  User_ID { get; set; }
        public long Kategorijas_ID { get; set; }
    }
}
