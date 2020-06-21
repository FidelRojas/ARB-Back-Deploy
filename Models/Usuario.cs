using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models
{
    public class Usuario : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ubication { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<Destinatario> Destinatarios { get; set; }
        public int cantPedidos { get; set; }
        public int cantDestinatarios { get; set; }
    }
}
