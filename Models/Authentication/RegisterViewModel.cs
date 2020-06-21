using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        public string Ubication { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}
