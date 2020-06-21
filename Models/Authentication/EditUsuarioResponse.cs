using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models.Authentication
{
    public class EditUsuarioResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Ubication { get; set; }
        public int Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string idUsuario { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}
