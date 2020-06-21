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
    [Route("api/usuarios/{userId:Guid}/pedidos")]
    public class PedidosController : ControllerBase
    {
        private IPedidoService pedidoService;
        public PedidosController(IPedidoService _pedidosService)
        {
            this.pedidoService = _pedidosService;
        }
        [Authorize(Roles = "Usuario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos(string userId)
        {
            try
            {
                var a = await pedidoService.GetAllPedidosAsync(userId);
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
        //public async Task<ActionResult<Pedido>> GetPedido(string userId, int id)
        //{
        //    try
        //    {
        //        var pedido = await this.pedidoService.GetPedidoAsync(userId, id);
        //        return Ok(pedido);
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
        [Authorize(Roles = "Usuario,Repartidor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> UpdatePedido(string userId, int id, [FromBody]Pedido pedido)
        {
            try
            {
                var pedidoUpdated = await this.pedidoService.UpdatePedidoAsync(userId, id, pedido);
                return Ok(pedidoUpdated);
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
        public async Task<ActionResult<Pedido>> CreatePedido(string userId, [FromBody] Pedido pedido)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postedPedido = await this.pedidoService.CreatePedido(userId, pedido);
            return Created($"/api/pedidos/{postedPedido.Id}", postedPedido);
        }

        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<bool>> DeletePedido(string userId, int id)
        //{
        //    try
        //    {
        //        return Ok(await this.pedidoService.DeletePedidoAsync(id));
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