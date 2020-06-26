using ARB.Exceptions;
using ARB.Models;
using ARB.Models.Authentication;
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
    
        [Route("api/[controller]")]
        [ApiController]
        public class UsuariosController : ControllerBase
        {
        private IUsuarioService usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [Authorize(Roles = "Repartidor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            try
            {
                var a = await usuarioService.GetUsuariosAsync();
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

        //[HttpDelete("{id:Guid}")]
        //public async Task<ActionResult<bool>> DeleteUsuario(string id)
        //{
        //    try
        //    {
        //        return Ok(await this.usuarioService.DeleteUsuarioAsync(id));
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

        //[HttpGet("{id:Guid}")]
        //public async Task<ActionResult<Usuario>> GetUsuario(string id)
        //{
        //    try
        //    {
        //        var user = await this.usuarioService.GetUsuarioAsync(id);
        //        return Ok(user);
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
        //[Authorize(Roles = "Usuario")]

        [Authorize(Roles = "Usuario")]
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Usuario>> UpdateUsuario(string id, [FromBody]Usuario usuario)
        {
            try
            {
                var userUpdated = await this.usuarioService.UpdateUsuarioAsync(id, usuario);
                return Ok(userUpdated);
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



        [Authorize(Roles = "Repartidor")]
        [HttpPost("Repartidor")]
        public async Task<IActionResult> RegisterRepartidorAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.RegisterUserAsync(model,"Repartidor");

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400


        }






        
        [HttpPost("User")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.RegisterUserAsync(model,"Usuario");

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400


        }
        [Authorize(Roles = "Repartidor")]
        [HttpPost("Role")]
        public async Task<IActionResult> CreateRolAsync([FromBody] CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.CreateRolAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }


        //[HttpPost("UserRole")]
        //public async Task<IActionResult> CreateUserRoleAsync([FromBody] CreateUserRoleViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await usuarioService.CreateUserRoleAsync(model);

        //        if (result.IsSuccess)
        //            return Ok(result); // Status Code: 200 

        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid"); // Status code: 400
        //}
        
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.LoginUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }
        
        [HttpPost("LoginRep")]
        public async Task<IActionResult> LoginRepartidor ([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await usuarioService.LoginRepartidorAsync(model);


                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

    }

}
