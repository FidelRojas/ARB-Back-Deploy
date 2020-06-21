using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models
{
    public class Repartidor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ubication { get; set; }
        public int Phone { get; set; }
        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
