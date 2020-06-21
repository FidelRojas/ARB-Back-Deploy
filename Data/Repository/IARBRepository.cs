using ARB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Repository
{
    public interface IARBRepository
    {
        Task<bool> SaveChangesAsync();

        //Destinatario
        Task<DestinatarioEntity> GetDestinatarioAsync(int id);
        void UpdateDestinatarioAsync(DestinatarioEntity destinatario);
        void CreateDestinatario(DestinatarioEntity destinatario);
        Task DeleteDestinatarioAsync(int id);
        Task<IEnumerable<DestinatarioEntity>> GetAllDestinatarios(string userId);

        //Pedido
        Task<PedidoEntity> GetPedidoAsync(int id);
        void UpdatePedidoAsync(PedidoEntity pedido);
        void CreatePedido(PedidoEntity pedido);
        Task DeletePedidoAsync(int id);
        Task<IEnumerable<PedidoEntity>> GetAllPedidosByUserId(string userId);
        Task<IEnumerable<PedidoEntity>> GetAllPedidosByRepartidorId(int repartidorId);
        Task<IEnumerable<PedidoEntity>> GetAllPedidosByDestiatarioId(int destinatarioId);

        //Usuario
        Task<UsuarioEntity> GetUsuarioAsync(string id);
        void UpdateUsuarioAsync(UsuarioEntity usuario);
        Task DeleteUsuarioAsync(string id);
        Task<IEnumerable<UsuarioEntity>> GetAllUsuarios();

        //Repartidor
        Task<RepartidorEntity> GetRepartidorAsync(int id);
        void UpdateRepartidorAsync(RepartidorEntity repartidor);
        void CreateRepartidor(RepartidorEntity repartidor);
        Task DeleteRepartidorAsync(int idRep);
        Task<IEnumerable<RepartidorEntity>> GetAllRepartidores();
        Task<IEnumerable<DestinatarioEntity>> GetAllDestinatariosByRepartidor();

        //SolictudUbicacion
        Task<SolicitudUbicacionEntity> GetSolicitudAsync(string id);
        Task DeleteSolicitudAsync(string id);
        void UpdateSolicitudAsync(SolicitudUbicacionEntity solicitud);
        void CreateSolicitud(SolicitudUbicacionEntity solicitud);
        Task<IEnumerable<SolicitudUbicacionEntity>> GetAllSolicitudes();
    }
}
