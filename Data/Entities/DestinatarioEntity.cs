using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class DestinatarioEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Ubication { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual UsuarioEntity Usuario { get; set; }
        public virtual IEnumerable<SolicitudUbicacionEntity> SolicitudesUbicacion { get; set; }
        public virtual IEnumerable<PedidoEntity> Pedidos { get; set; }
    }
}
