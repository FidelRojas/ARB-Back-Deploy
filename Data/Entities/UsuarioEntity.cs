using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class UsuarioEntity:IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ubication { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        [Required]
        public int Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual IEnumerable<PedidoEntity> Pedidos { get; set; }
        public virtual IEnumerable<DestinatarioEntity> Destinatarios { get; set; }
    }
}
