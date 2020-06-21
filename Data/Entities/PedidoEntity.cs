using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class PedidoEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime DateOfDelivery { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Cost { get; set; }
        public string Status { get; set; }



        [ForeignKey("UsuarioId")]
        public virtual UsuarioEntity Usuario { get; set; }
        [ForeignKey("DestinatarioId")]
        public virtual DestinatarioEntity Destinatario { get; set; }
        [ForeignKey("RepartidorId")]
        public virtual RepartidorEntity Repartidor { get; set; }
    }
}
