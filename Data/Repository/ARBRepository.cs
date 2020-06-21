using Microsoft.EntityFrameworkCore;
using ARB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Repository
{
    public class ARBRepository : IARBRepository
    {
        private ARBDbContext ARBDbContext;

        public ARBRepository(ARBDbContext ARBDbContext)
        {
            this.ARBDbContext = ARBDbContext;
        }

        //ADD
        public void CreateDestinatario(DestinatarioEntity destinatario)
        {
            ARBDbContext.Entry(destinatario.Usuario).State = EntityState.Unchanged;
            ARBDbContext.Destinatarios.Add(destinatario);
        }

        public void CreatePedido(PedidoEntity Pedido)
        {
            ARBDbContext.Entry(Pedido.Usuario).State = EntityState.Unchanged;
            ARBDbContext.Entry(Pedido.Destinatario).State = EntityState.Unchanged;
            ARBDbContext.Entry(Pedido.Repartidor).State = EntityState.Unchanged;
            ARBDbContext.Pedidos.Add(Pedido);
        }

        public void CreateRepartidor(RepartidorEntity repartidor)
        {
            ARBDbContext.Repartidores.Add(repartidor);
        }

        public void CreateSolicitud(SolicitudUbicacionEntity solicitud)
        {
            ARBDbContext.Entry(solicitud.Destinatario).State = EntityState.Unchanged;
            ARBDbContext.SolicitudesUbicaciones.Add(solicitud);
        }

        //DELETE
        public async Task DeleteDestinatarioAsync(int id)
        {
            var destinatarioToDelete = await ARBDbContext.Destinatarios.SingleAsync(d => d.Id == id);
            ARBDbContext.Destinatarios.Remove(destinatarioToDelete);
        }

        public async Task DeletePedidoAsync(int id)
        {
            var pedidoToDelete = await ARBDbContext.Pedidos.SingleAsync(p => p.Id == id);
            ARBDbContext.Pedidos.Remove(pedidoToDelete);
        }

        public async Task DeleteRepartidorAsync(int idRep)
        {
            var repartidorToDelete = await ARBDbContext.Repartidores.SingleAsync(r => r.Id == idRep);
            ARBDbContext.Repartidores.Remove(repartidorToDelete);
        }

        public async Task DeleteSolicitudAsync(string id)
        {
            var solicitudToDelete = await ARBDbContext.SolicitudesUbicaciones.SingleAsync(u => u.Id == id);
            ARBDbContext.SolicitudesUbicaciones.Remove(solicitudToDelete);
        }

        public async Task DeleteUsuarioAsync(string id)
        {
            var usuarioToDelete = await ARBDbContext.Usuarios.SingleAsync(u => u.Id == id);
            ARBDbContext.Usuarios.Remove(usuarioToDelete);
        }

        //GET-TODOS
        public async Task<IEnumerable<DestinatarioEntity>> GetAllDestinatarios(string userId)
        {
            IQueryable<DestinatarioEntity> query = ARBDbContext.Destinatarios.Where(d => d.Usuario.Id == userId);
            query = query.AsNoTracking().Where(d => d.Usuario.Id == userId);
            query = query.Include(d => d.Pedidos);
            query = query.Include(d => d.Usuario);
            //query = query.Include(d => d.SolicitudUbicacion);
            var a = await query.ToArrayAsync();
            return a;
        }

        public async Task<IEnumerable<DestinatarioEntity>> GetAllDestinatariosByRepartidor()
        {
            IQueryable<DestinatarioEntity> query = ARBDbContext.Destinatarios;
            query = query.AsNoTracking();
            query = query.Include(d => d.Pedidos);
            query = query.Include(d => d.Usuario);
            //query = query.Include(d => d.SolicitudUbicacion);
            var a = await query.ToArrayAsync();
            return a;
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllPedidosByDestiatarioId(int destinatarioId)
        {
            IQueryable<PedidoEntity> query = ARBDbContext.Pedidos.Where(p => p.Destinatario.Id == destinatarioId);
            query = query.Include(q => q.Destinatario);
            query = query.Include(q => q.Usuario);
            query = query.Include(q => q.Repartidor);
            //query = query.AsNoTracking().Where(p => p.Usuario.Id == destinatarioId); NO SE QUE PASA AQUIIIII!!!!!!!!!!
            var a = await query.ToArrayAsync();
            return a;
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllPedidosByRepartidorId(int repartidorId)
        {
            IQueryable<PedidoEntity> query = ARBDbContext.Pedidos.Where(p => p.Repartidor.Id == repartidorId);
            query = query.Include(q => q.Destinatario);
            query = query.Include(q => q.Usuario);
            query = query.Include(q => q.Repartidor);
            //query = query.AsNoTracking().Where(p => p.Usuario.Id == repartidorId); NO SE QUE PASA AQUIIIII!!!!!!!!!!
            var a = await query.ToArrayAsync();
            return a;
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllPedidosByUserId(string userId)
        {
            IQueryable<PedidoEntity> query = ARBDbContext.Pedidos.Where(p => p.Usuario.Id == userId);
            query = query.Include(q => q.Destinatario);
            query = query.Include(q => q.Usuario);
            query = query.Include(q => q.Repartidor);
            //query = query.AsNoTracking().Where(p => p.Usuario.Id == userId);
            var a = await query.ToArrayAsync();
            return a;
        }

        public async Task<IEnumerable<RepartidorEntity>> GetAllRepartidores()
        {
            IQueryable<RepartidorEntity> query = ARBDbContext.Repartidores;
            query = query.Include(r => r.Pedidos);
            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<SolicitudUbicacionEntity>> GetAllSolicitudes()
        {
            IQueryable<SolicitudUbicacionEntity> query = ARBDbContext.SolicitudesUbicaciones;
            query = query.Include(s=>s.Destinatario);
            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<UsuarioEntity>> GetAllUsuarios()
        {
            IQueryable<UsuarioEntity> query = ARBDbContext.Usuarios;
            query = query.Include(u => u.Pedidos);
            query = query.Include(u => u.Destinatarios);
            return await query.ToArrayAsync();
        }

        //GET-BY-ID
        public async Task<DestinatarioEntity> GetDestinatarioAsync(int id)
        {
            IQueryable<DestinatarioEntity> query = ARBDbContext.Destinatarios;
            query = query.AsNoTracking();
            query = query.Include(d => d.Pedidos);
            //query = query.Include(d => d.SolicitudUbicacion);
            return await query.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<PedidoEntity> GetPedidoAsync(int id)
        {
            IQueryable<PedidoEntity> query = ARBDbContext.Pedidos;
            query = query.Include(p => p.Usuario);
            query = query.Include(p => p.Destinatario);
            query = query.Include(p => p.Repartidor);
            query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<RepartidorEntity> GetRepartidorAsync(int id)
        {
            IQueryable<RepartidorEntity> query = ARBDbContext.Repartidores;
            query = query.AsNoTracking();
            query = query.Include(r => r.Pedidos);
            return await query.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<SolicitudUbicacionEntity> GetSolicitudAsync(string id)
        {
            IQueryable<SolicitudUbicacionEntity> query = ARBDbContext.SolicitudesUbicaciones;
            query = query.AsNoTracking();
            query = query.Include(s=>s.Destinatario);
            return await query.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<UsuarioEntity> GetUsuarioAsync(string id)
        {
            IQueryable<UsuarioEntity> query = ARBDbContext.Usuarios;
            query = query.AsNoTracking();
            query = query.Include(u => u.Pedidos);
            query = query.Include(u => u.Destinatarios);
            return await query.SingleOrDefaultAsync(u => u.Id == id);
        }

        //SAVE
        public async Task<bool> SaveChangesAsync()
        {
            return (await ARBDbContext.SaveChangesAsync()) > 0;
        }

        //UPDATE
        public void UpdateDestinatarioAsync(DestinatarioEntity destinatario)
        {
            ARBDbContext.Entry(destinatario.Usuario).State = EntityState.Unchanged;
            ARBDbContext.Destinatarios.Update(destinatario);
        }

        public void UpdatePedidoAsync(PedidoEntity Pedido)
        {
            ARBDbContext.Entry(Pedido.Usuario).State = EntityState.Unchanged;
            ARBDbContext.Entry(Pedido.Destinatario).State = EntityState.Unchanged;
            ARBDbContext.Pedidos.Update(Pedido);
        }

        public void UpdateRepartidorAsync(RepartidorEntity repartidor)
        {
            ARBDbContext.Repartidores.Update(repartidor);
        }

        public void UpdateSolicitudAsync(SolicitudUbicacionEntity solicitud)
        {
            ARBDbContext.Entry(solicitud.Destinatario).State = EntityState.Unchanged;
            ARBDbContext.SolicitudesUbicaciones.Update(solicitud);
        }

        public void UpdateUsuarioAsync(UsuarioEntity Usuario)
        {
            var a = ARBDbContext.Usuarios.Update(Usuario);
       
        }
    }
}
