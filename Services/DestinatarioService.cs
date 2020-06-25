using ARB.Data.Entities;
using ARB.Data.Repository;
using ARB.Exceptions;
using ARB.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public class DestinatarioService: IDestinatarioService
    {
        private IARBRepository ARBRepository;
        private readonly IMapper mapper;
        public DestinatarioService(IARBRepository _ARBRepository, IMapper _mapper)
        {
            this.ARBRepository = _ARBRepository;
            this.mapper = _mapper;
        }

        private async Task validateUserId(string userId)
        {
            var userEntity = await ARBRepository.GetUsuarioAsync(userId);
            if (userEntity == null)
            {
                throw new NotFoundItemException($"cannot found user with id:{userId}");
            }
        }

        public async Task<Destinatario> CreateDestinatario(string userId, Destinatario destinatario)
        {
            destinatario.UsuarioId = userId;
            destinatario.latitude = "NoTiene";
            destinatario.longitude = "NoTiene";
            await validateUserId(userId);
            var destinatarioEntity = mapper.Map<DestinatarioEntity>(destinatario);

            ARBRepository.CreateDestinatario(destinatarioEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Destinatario>(destinatarioEntity);
            }
            throw new Exception("there where and error with the DB");
        }

        public async Task<bool> DeleteDestinatarioAsync(int id)
        {
            await ARBRepository.DeleteDestinatarioAsync(id);
            if (await ARBRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Destinatario>> GetAllDestinatariosAsync(string userId)
        {
            var destinatariosEntities = await ARBRepository.GetAllDestinatarios(userId);
            var res = mapper.Map<IEnumerable<Destinatario>>(destinatariosEntities);
            for (int i = 0; i < res.Count(); i++)
            {
                res.ElementAt(i).cantPedidos = res.ElementAt(i).Pedidos.Count();
            }
            return res;
            //foreach (Destinatario d in res) {
            //    //d.solicitudUbicacionId = destinatariosEntities.ToList().Find(de => de.Id == d.Id).SolicitudUbicacion.Id;
            //}
            //return res;
        }

        public async Task<Destinatario> GetDestinatarioAsync(string userId, int id)
        {
            var destinatarioEntity = await ARBRepository.GetDestinatarioAsync(id);
            if (destinatarioEntity == null)
            {
                throw new NotFoundItemException("Destination not found");
            }
            return mapper.Map<Destinatario>(destinatarioEntity);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosPendientes(string userId, int id)
        {
            await validateUserId(userId);
            var pedidosEntities = (await ARBRepository.GetAllPedidosByDestiatarioId(id));
            var pedidos = mapper.Map<IEnumerable<Pedido>>(pedidosEntities).Where(p => (p.Status == "Creado") && (p.DestinatarioId == id));
            return pedidos;
        }

        public async Task<Destinatario> UpdateDestinatarioAsync(string userId, int id, Destinatario destinatario)
        {
            if (id != destinatario.Id)
            {
                throw new NotFoundItemException($"not found destination with id:{id}");
            }
            await validateUserId(userId);
            destinatario.UsuarioId = userId;
            var destEntity = mapper.Map<DestinatarioEntity>(destinatario);
            ARBRepository.UpdateDestinatarioAsync(destEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Destinatario>(destEntity);
            }
            throw new Exception("there were an error with the BD");
        }

        //public async Task<IEnumerable<Pedido>> GetPedidosPendientes(string userId, int id)
        //{
        //    await validateUserId(userId);
        //    var pedidosEntities = (await ARBRepository.GetAllPedidosByDestiatarioId(id));
        //    var pedidos = mapper.Map<IEnumerable<Pedido>>(pedidosEntities).Where(p => (p.Status == "Creado") && (p.DestinatarioId == id));
        //    return pedidos;
        //}

        public async Task<Destinatario> UpdateDestinatarioBySolicitud(SolicitudUbicacion solicitud)
        {
            var destinatariosEntity = await ARBRepository.GetAllDestinatariosByRepartidor();
            var destinatarioEntity = destinatariosEntity.ToList().Find(d => d.Id == solicitud.DestinatarioId);
            destinatarioEntity.latitude = solicitud.latitude;
            destinatarioEntity.longitude = solicitud.longitude;
            destinatarioEntity.Pedidos = null;
            var destinatario = mapper.Map<Destinatario>(destinatarioEntity);
            return await UpdateDestinatarioAsync(destinatario.UsuarioId, destinatario.Id, destinatario);
        }
    }
}
