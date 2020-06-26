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
    [Route("api/[controller]")]
    public class RepartidoresController : ControllerBase
    {
        IRepartidorService repartidorService;
        public RepartidoresController(IRepartidorService _repartidorService)
        {
            this.repartidorService = _repartidorService;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Repartidor>>> GetRepartidores()
        //{
        //    try
        //    {
        //        var a = await repartidorService.GetAllRepartidoresAsync();
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

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Repartidor>> GetRepartidor(int id)
        //{
        //    try
        //    {
        //        var repartidor = await this.repartidorService.GetRepartidorAsync(id);
        //        return Ok(repartidor);
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

        [Authorize(Roles = "Repartidor")]
        [HttpGet("{id:int}/destinatarios")]
        public async Task<ActionResult<Destinatario>> GetAllDestinatarios(int id)
        {
            try
            {
                var destinatarios = await this.repartidorService.GetDestinatariosByRepartidor();
                return Ok(destinatarios);
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
        [HttpGet("{id:int}/pedidos")]
        public async Task<ActionResult<Repartidor>> GetPedidosbyRepartidorId(int id)
        {
            try
            {
                var repartidor = await this.repartidorService.GetAllPedidosAsync(id);
                return Ok(repartidor);
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

        //[HttpGet("{id:int}/pedidos/{idPedido:int}")]
        //public async Task<ActionResult<Repartidor>> GetPedidobyRepartidorId(int id, int idPedido)
        //{
        //    try
        //    {
        //        var repartidor = await this.repartidorService.GetPedidoAsync(idPedido);
        //        return Ok(repartidor);
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

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Repartidor>> UpdateRepartidor(int id, [FromBody]Repartidor repartidor)
        //{
        //    try
        //    {
        //        var repartidorUpdated = await this.repartidorService.UpdateRepartidorAsync(id, repartidor);
        //        return Ok(repartidorUpdated);
        //    }
        //    catch (NotFoundItemException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost]
        [Authorize(Roles = "Repartidor")]
        public async Task<ActionResult<Repartidor>> PostRepartidor([FromBody] Repartidor repartidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postedRepartidor = await this.repartidorService.CreateRepartidor(repartidor);
            return Created($"/api/repartidor/{postedRepartidor.Id}", postedRepartidor);
        }

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<bool>> DeleteRepartidor(int id)
        //{
        //    try
        //    {
        //        return Ok(await this.repartidorService.DeleteRepartidorAsync(id));
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
    }
}