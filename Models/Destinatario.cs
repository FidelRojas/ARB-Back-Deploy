using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models
{
    public class Destinatario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Ubication { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
        public IEnumerable<Pedido> solicitudesUbicacion { get; set; }
        public int cantPedidos { get; set; }
        public string UsuarioId { get; set; }
    }
}
