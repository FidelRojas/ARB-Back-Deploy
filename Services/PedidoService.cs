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
    public class PedidoService:IPedidoService
    {
        private IARBRepository ARBRepository;
        private readonly IMapper mapper;
        public PedidoService(IARBRepository _ARBRepository, IMapper _mapper)
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

        public async Task<Pedido> CreatePedido(string userId, Pedido pedido)
        {
            pedido.UsuarioId = userId;
            pedido.Status = "Creado";
            pedido.RepartidorId = 1;
            await validateUserId(userId);
            var pedidoEntity = mapper.Map<PedidoEntity>(pedido);
            ARBRepository.CreatePedido(pedidoEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Pedido>(pedidoEntity);
            }
            throw new Exception("there where and error with the DB");
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            await ARBRepository.DeletePedidoAsync(id);
            if (await ARBRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync(string userId)
        {
            var pedidosEntities = await ARBRepository.GetAllPedidosByUserId(userId);
            var res = mapper.Map<IEnumerable<Pedido>>(pedidosEntities);
            foreach (Pedido p in res)
            {
               // p.DestinatarioName = pedidosEntities.ToList().Find(ped => ped.Id == p.Id).Destinatario.Name;
                p.RepartidorId = 1;
            }
            return res;
        }

        public async Task<Pedido> GetPedidoAsync(string userId, int id)
        {
            var pedidoEntity = await ARBRepository.GetPedidoAsync(id);
            if (pedidoEntity == null)
            {
                throw new NotFoundItemException("Pedido not found");
            }
            var pedido = mapper.Map<Pedido>(pedidoEntity);
            //pedido.DestinatarioName = mapper.Map<Destinatario>(await ARBRepository.GetDestinatarioAsync(pedido.DestinatarioId)).Name;
            //pedido.RepartidorId = pedidoEntity.Repartidor.Id;
            pedido.RepartidorId = 1;

            return pedido;
        }

        public async Task<Pedido> UpdatePedidoAsync(string userId, int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                throw new NotFoundItemException($"not found pedido with id:{id}");
            }
            await validateUserId(userId);
            pedido.UsuarioId = userId;
            var pedidoEntity = mapper.Map<PedidoEntity>(pedido);
            ARBRepository.UpdatePedidoAsync(pedidoEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<Pedido>(pedidoEntity);
            }
            throw new Exception("there were an error with the BD");
        }

    }
}
