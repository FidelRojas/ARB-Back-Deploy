using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Status { get; set; }
        public string UsuarioId { get; set; }
        public int DestinatarioId { get; set; }
        public int RepartidorId { get; set; }
        public string DestinatarioName { get; set; }
    }
}
