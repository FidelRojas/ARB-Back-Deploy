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
    public class RepartidorService : IRepartidorService
    {
        private IARBRepository ARBRepository;
        private readonly IMapper mapper;

        public RepartidorService(IARBRepository _ARBRepository, IMapper _mapper)
        {
            this.ARBRepository = _ARBRepository;
            this.mapper = _mapper;
        }

        private async Task validatePedidoId(int pedidoId)
        {
            var pedidoEntity = await ARBRepository.GetPedidoAsync(pedidoId);
            if (pedidoEntity == null)
            {
                throw new NotFoundItemException($"cannot found pedido with id:{pedidoId}");
            }
        }

        public async Task<Repartidor> CreateRepartidor(Repartidor repartidor)
        {
            var repartidorEntity = mapper.Map<RepartidorEntity>(repartidor);
            ARBRepository.CreateRepartidor(repartidorEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Repartidor>(repartidorEntity);
            }
            throw new Exception("there where and error with the DB");
        }

        public async Task<bool> DeleteRepartidorAsync(int id)
        {
            await ARBRepository.DeleteRepartidorAsync(id);
            if (await ARBRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync(int idRepartidor)
        {
            var pedidosEntities = await ARBRepository.GetAllPedidosByRepartidorId(idRepartidor);
            var res = mapper.Map<IEnumerable<Pedido>>(pedidosEntities);
            foreach (Pedido p in res)
            {
                p.RepartidorId = pedidosEntities.ToList().Find(ped => ped.Id == p.Id).Repartidor.Id;
            }
            return res;
        }

        public async Task<IEnumerable<Repartidor>> GetAllRepartidoresAsync()
        {
            var repartidorEntities = await ARBRepository.GetAllRepartidores();
            var res = mapper.Map<IEnumerable<Repartidor>>(repartidorEntities);
            return res;
        }

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            var pedidoEntity = await ARBRepository.GetPedidoAsync(id);
            if (pedidoEntity == null)
            {
                throw new NotFoundItemException("Pedido not found");
            }
            var pedido = mapper.Map<Pedido>(pedidoEntity);
            pedido.RepartidorId = pedidoEntity.Repartidor.Id;
            return pedido;
        }

        public async Task<Repartidor> GetRepartidorAsync(int id)
        {
            var repartidorEntity = await ARBRepository.GetRepartidorAsync(id);
            if (repartidorEntity == null)
            {
                throw new NotFoundItemException("Repartidor not found");
            }
            var res = mapper.Map<Repartidor>(repartidorEntity);
            return res;
        }

        public async Task<Repartidor> UpdateRepartidorAsync(int id, Repartidor repartidor)
        {
            if (id != repartidor.Id)
            {
                throw new NotFoundItemException($"not found repartidor with id:{id}");
            }
            var repartidorEntity = mapper.Map<RepartidorEntity>(repartidor);
            ARBRepository.UpdateRepartidorAsync(repartidorEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Repartidor>(repartidorEntity);
            }
            throw new Exception("there were an error with the BD");
        }

        public async Task<IEnumerable<Destinatario>> GetDestinatariosByRepartidor()
        {
            var destinatariosEntities = await ARBRepository.GetAllDestinatariosByRepartidor();
            var res = mapper.Map<IEnumerable<Destinatario>>(destinatariosEntities);
            return res;
        }
    }
}
