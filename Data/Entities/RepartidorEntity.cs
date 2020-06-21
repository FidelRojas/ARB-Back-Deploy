using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class RepartidorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public string Ubication { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public IEnumerable<PedidoEntity> Pedidos { get; set; }
    }
}
