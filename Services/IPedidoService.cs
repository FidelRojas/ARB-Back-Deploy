using ARB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllPedidosAsync(string userId);
        Task<Pedido> GetPedidoAsync(string userId, int id);
        Task<Pedido> UpdatePedidoAsync(string userId, int id, Pedido pedido);
        Task<Pedido> CreatePedido(string userId, Pedido pedido);
        Task<bool> DeletePedidoAsync(int id);
    }
}
