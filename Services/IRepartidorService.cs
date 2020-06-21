using ARB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public interface IRepartidorService
    {
        Task<IEnumerable<Repartidor>> GetAllRepartidoresAsync();
        Task<Repartidor> GetRepartidorAsync(int id);
        Task<Repartidor> UpdateRepartidorAsync(int id, Repartidor repartidor);
        Task<Repartidor> CreateRepartidor(Repartidor repartidor);
        Task<bool> DeleteRepartidorAsync(int id);
        Task<IEnumerable<Pedido>> GetAllPedidosAsync(int idRepartidor);
        Task<Pedido> GetPedidoAsync(int id);
        Task<IEnumerable<Destinatario>> GetDestinatariosByRepartidor();
    }
}
