using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class SolicitudUbicacionEntity
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [ForeignKey("DestinatarioId")]
        public virtual DestinatarioEntity Destinatario { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public bool isEdited { get; set; }
        public DateTime fechaCreada { get; set; }
    }
}
