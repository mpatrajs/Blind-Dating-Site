﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public virtual Profile Profile { get; set; }
    }
}