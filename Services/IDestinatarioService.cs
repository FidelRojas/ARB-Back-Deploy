using ARB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public interface IDestinatarioService
    {
        Task<IEnumerable<Destinatario>> GetAllDestinatariosAsync(string userId);
        Task<Destinatario> GetDestinatarioAsync(string userId, int id);
        Task<Destinatario> UpdateDestinatarioAsync(string userId, int id, Destinatario destinatario);
        Task<Destinatario> UpdateDestinatarioBySolicitud(SolicitudUbicacion solicitud);
        Task<Destinatario> CreateDestinatario(string userId, Destinatario destinatario);
        Task<bool> DeleteDestinatarioAsync(int id);
        Task<IEnumerable<Pedido>> GetPedidosPendientes(string userId, int id);

    }
}
