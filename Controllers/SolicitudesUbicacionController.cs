using ARB.Exceptions;
using ARB.Models;
using ARB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Controllers
{
    [Route("api/destino/{idDestino:int}/solicitudes")]
    [ApiController]
    public class SolicitudesUbicacionController : ControllerBase
    {
        private ISolicitudUbicacionService solicitudService;
        private IDestinatarioService destinatarioService;

        public SolicitudesUbicacionController(ISolicitudUbicacionService _solicitudService, IDestinatarioService _destinatarioService)
        {
            this.solicitudService = _solicitudService;
            this.destinatarioService = _destinatarioService;
        }
        //[Authorize(Roles = "Usuario")]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<SolicitudUbicacion>>> GetSolicitudes()
        //{
        //    try
        //    {
        //        var a = await solicitudService.GetAllSolicitudes();
        //        return Ok(a);

        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");

        //    }
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudUbicacion>> GetSolicitudById(string id)
        {
            try
            {
                var x = await this.solicitudService.GetSolicitudAsync(id);
                return Ok(x);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "something bad happend");
            }
        }
        [Authorize(Roles = "Repartidor")]
        [HttpPost]
        public async Task<ActionResult<SolicitudUbicacion>> PostSolicitud(int idDestino)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postedSolicitud = await this.solicitudService.CreateSolicitud(idDestino);
            return postedSolicitud;
        }
        //[Authorize(Roles = "Usuario")]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteSolicitud(int idDestino, string id)
        //{
        //    try
        //    {
        //        return Ok(await this.solicitudService.DeleteSolicitudAsync(id));
        //    }
        //    catch (NotFoundItemException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<SolicitudUbicacion>> UpdateUbicacion(int idDestino, string id, [FromBody]SolicitudUbicacion solicitud)
        {
            try
            {
                var solicitudUpdated = await this.solicitudService.UpdateSolicitudAsync(idDestino, id, solicitud);
                return Ok(solicitudUpdated);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/d")]
        public async Task<ActionResult<SolicitudUbicacion>> UpdateUbicacionDestinatario(int idDestino, string id, [FromBody]SolicitudUbicacion solicitud)
        {
            try
            {
                var destinatarioUpdated = await this.destinatarioService.UpdateDestinatarioBySolicitud(solicitud);
                return Ok(destinatarioUpdated);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
