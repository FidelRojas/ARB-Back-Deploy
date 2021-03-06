﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
