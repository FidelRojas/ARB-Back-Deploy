using ARB.Data.Entities;
using ARB.Data.Repository;
using ARB.Exceptions;
using ARB.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ARB.Services
{
    public class SolicitudUbicacionService : ISolicitudUbicacionService
    {
        private IConfiguration configuration;
        private IARBRepository ARBRepository;
        private readonly IMapper mapper;
        public SolicitudUbicacionService(IARBRepository _ARBRepository, IMapper _mapper, IConfiguration configuration)
        {
            this.ARBRepository = _ARBRepository;
            this.mapper = _mapper;
            this.configuration = configuration;
        }

        private async Task validateDestinoId(int destinoId)
        {
            var destinoEntity = await ARBRepository.GetDestinatarioAsync(destinoId);
            if (destinoEntity == null)
            {
                throw new NotFoundItemException($"cannot found destinatario with id:{destinoId}");
            }
        }
        
        public async Task<IEnumerable<SolicitudUbicacion>> GetAllSolicitudes()
        {
            var solicitudesEntities = await ARBRepository.GetAllSolicitudes();
            var res = mapper.Map<IEnumerable<SolicitudUbicacion>>(solicitudesEntities);
            foreach (SolicitudUbicacion su in res)
            {
                su.DestinatarioId = solicitudesEntities.ToList().Find(s => s.Id == su.Id).Destinatario.Id;
                su.destinatarioName = solicitudesEntities.ToList().Find(s => s.Id == su.Id).Destinatario.Name;
            }
            return res;
        }

        public async Task<SolicitudUbicacion> GetSolicitudAsync(string id)
        {
            var solicitudEntity = await ARBRepository.GetSolicitudAsync(id);
            if (solicitudEntity == null)
            {
                throw new NotFoundItemException("Solicitud not found");
            }
            var solicitud = mapper.Map<SolicitudUbicacion>(solicitudEntity);
            solicitud.DestinatarioId = solicitudEntity.Destinatario.Id;
            solicitud.destinatarioName = solicitudEntity.Destinatario.Name;
            return solicitud;
        }

        public async Task<SolicitudUbicacion> UpdateSolicitudAsync(int destinatarioId, string id, SolicitudUbicacion solicitud)
        {
            if (id != solicitud.Id)
            {
                throw new NotFoundItemException($"not found solicitud with id:{id}");
            }
            await validateDestinoId(destinatarioId);
            solicitud.Id = id;
            solicitud.isEdited = true;

            var solicitudEntity = mapper.Map<SolicitudUbicacionEntity>(solicitud);

            ARBRepository.UpdateSolicitudAsync(solicitudEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<SolicitudUbicacion>(solicitudEntity);
            }
            throw new Exception("there were an error with the BD");
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public async Task<SolicitudUbicacion> CreateSolicitud(int idDestino)
        {
            SolicitudUbicacionEntity solicitudEntity = new SolicitudUbicacionEntity();
            //solicitud.DestinatarioId = idDestino;
            solicitudEntity.fechaCreada = DateTime.Now;
            solicitudEntity.Destinatario = await ARBRepository.GetDestinatarioAsync(idDestino);
            solicitudEntity.Id = GetHashString(solicitudEntity.Destinatario.Id + solicitudEntity.fechaCreada.ToString());
            solicitudEntity.isEdited = false;
            solicitudEntity.latitude = "0";
            solicitudEntity.longitude = "0";

            ARBRepository.CreateSolicitud(solicitudEntity);
            if (await ARBRepository.SaveChangesAsync())
            {
                return mapper.Map<SolicitudUbicacion>(solicitudEntity);
            }
            throw new Exception("there were an error with the BD");

        }

        public async Task<bool> DeleteSolicitudAsync(string id)
        {
            await ARBRepository.DeleteSolicitudAsync(id);
            if (await ARBRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }
    }
}
