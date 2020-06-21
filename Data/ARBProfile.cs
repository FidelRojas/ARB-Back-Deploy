using AutoMapper;
using ARB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARB.Models;

namespace ARB.Data
{
    public class ARBProfile : Profile
    {
        public ARBProfile()
        {
            this.CreateMap<DestinatarioEntity, Destinatario>()
                .ReverseMap();
            this.CreateMap<PedidoEntity, Pedido>()
             .ReverseMap();
            this.CreateMap<UsuarioEntity, Usuario>()
                .ReverseMap();
            this.CreateMap<RepartidorEntity, Repartidor>()
                .ReverseMap();
            this.CreateMap<SolicitudUbicacionEntity, SolicitudUbicacion>()
                .ReverseMap();
        }
    }
}
