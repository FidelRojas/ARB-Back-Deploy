using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Models
{
    public class SolicitudUbicacion
    {
        public string Id { get; set; }
        public int DestinatarioId { get; set; }
        public string destinatarioName { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public bool isEdited { get; set; }
        public DateTime fechaCreada { get; set; }
    }
}
