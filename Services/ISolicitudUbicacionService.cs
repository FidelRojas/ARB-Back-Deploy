using ARB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public interface ISolicitudUbicacionService
    {
        Task<SolicitudUbicacion> GetSolicitudAsync(string id);
        Task<SolicitudUbicacion> UpdateSolicitudAsync(int destinatarioId, string id, SolicitudUbicacion solicitud);
        Task<SolicitudUbicacion> CreateSolicitud(int idDestino);
        Task<IEnumerable<SolicitudUbicacion>> GetAllSolicitudes();
        Task<bool> DeleteSolicitudAsync(string id);
    }
}
