using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models.Authentication
{
    public class LoginRepartidorResponse
    {
        public string idRepartidor { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ubication { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
