using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARB.Exceptions;
using ARB.Models;
using ARB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARB.Controllers
{
    //[Authorize(Roles = "Usuario")]
    [Route("api/usuarios/{userId:Guid}/destinatarios")]
    [ApiController]
    
    public class DestinatariosController : ControllerBase
    {

        private IDestinatarioService destinatarioService;

        public DestinatariosController(IDestinatarioService _destinatarioService)
        {
            this.destinatarioService = _destinatarioService;
        }
        [Authorize(Roles = "Usuario,Repartidor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destinatario>>> GetDestinatarios(string userId)
        {
            try
            {
                var a = await destinatarioService.GetAllDestinatariosAsync(userId);
                return Ok(a);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");

            }
        }
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Destinatario>> GetDestinatario(string userId, int id)
        //{
        //    try
        //    {
        //        var destinatario = await this.destinatarioService.GetDestinatarioAsync(userId, id);
        //        return Ok(destinatario);
        //    }
        //    catch (NotFoundItemException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "something bad happend");
        //    }
        //}
        //[HttpGet("{id:int}/pedidos-pendientes")]    
        //public async Task<ActionResult<Pedido>> GetPedidosPedientes(string userId, int id)
        //{
        //    try
        //    {
        //        var destinatario = await this.destinatarioService.GetPedidosPendientes(userId, id);
        //        return Ok(destinatario);
        //    }
        //    catch (NotFoundItemException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "something bad happend");
        //    }
        //}
        [Authorize(Roles = "Usuario")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Destinatario>> UpdateDestinatario(string userId, int id, [FromBody]Destinatario destinatario)
        {
            try
            {
                var destinatarioUpdated = await this.destinatarioService.UpdateDestinatarioAsync(userId, id, destinatario);
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
        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public async Task<ActionResult<Destinatario>> PostDestinatario(string userId, [FromBody] Destinatario destinatario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postedDestinatario = await this.destinatarioService.CreateDestinatario(userId, destinatario);
            return Created($"/api/destinatarios/{postedDestinatario.Id}", postedDestinatario);
        }
        [Authorize(Roles = "Usuario")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteDestinatario(int id)
        {
            try
            {
                return Ok(await this.destinatarioService.DeleteDestinatarioAsync(id));
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }
    }
}