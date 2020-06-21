using ARB.Data.Entities;
using ARB.Data.Repository;
using ARB.Exceptions;
using ARB.Models;
using ARB.Models.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ARB.Services
{
    public class UsuarioService : IUsuarioService
    {
        private UserManager<UsuarioEntity> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IConfiguration configuration;
        private IARBRepository ARBRepository;
        private readonly IMapper mapper;
        public UsuarioService(UserManager<UsuarioEntity> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IARBRepository _ARBRepository, IMapper _mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.ARBRepository = _ARBRepository;
            this.mapper = _mapper;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuarioEntities = await ARBRepository.GetAllUsuarios();
            var res = mapper.Map<IEnumerable<Usuario>>(usuarioEntities);
            for (int i = 0; i < res.Count(); i++)
            {
                res.ElementAt(i).cantPedidos = res.ElementAt(i).Pedidos.Count();
                res.ElementAt(i).cantDestinatarios = res.ElementAt(i).Destinatarios.Count();
            }
            return res;
        }
        public async Task<bool> DeleteUsuarioAsync(string id)
        {
            await ARBRepository.DeleteUsuarioAsync(id);
            if (await ARBRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<Usuario> GetUsuarioAsync(string id)
        {
            var userEntity = await ARBRepository.GetUsuarioAsync(id);
            if (userEntity == null)
            {
                throw new NotFoundItemException("User not found");
            }
            var res = mapper.Map<Usuario>(userEntity);
            res.cantPedidos = res.Pedidos.Count();
            res.cantDestinatarios = res.Destinatarios.Count();
            return res;
        }

        public async Task<EditUsuarioResponse> UpdateUsuarioAsync(string id, Usuario usuario)
        {
            if (usuario == null)
            {
                throw new NullReferenceException("model is null");
            }
            var identityuser = await userManager.FindByIdAsync(id);
            identityuser.Name = usuario.Name;
            identityuser.LastName = usuario.LastName;
            identityuser.UserName = usuario.Name.Replace(" ", string.Empty).Trim();
            identityuser.Ubication = usuario.Ubication;
            identityuser.Phone = usuario.Phone;
            identityuser.latitude = usuario.latitude;
            identityuser.longitude = usuario.longitude;
            var result = await userManager.UpdateAsync(identityuser);

            if (result.Succeeded)
            {
                return new EditUsuarioResponse
                {
                    Message = "User updated successfully",
                    IsSuccess = true,
                    Ubication = identityuser.Ubication,
                    Phone = identityuser.Phone,
                    BirthDate = identityuser.BirthDate,
                    Email = identityuser.Email,
                    idUsuario = identityuser.Id,
                    name = identityuser.Name,
                    lastName = identityuser.LastName,
                    longitude = identityuser.longitude,
                    latitude = identityuser.latitude

                };
            }
            return new EditUsuarioResponse
            {
                Message = "User did not updated",
                IsSuccess = false
            };
        }

        public async Task<LoginRespone> LoginUserAsync(LoginViewModel model)
        {


            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null )
            {
                return new LoginRespone
                {
                    Message = "Usuario o contraseña incorrecto",
                    IsSuccess = false,
                };
            }

            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new LoginRespone
                {
                    Message = "Usuario o contraseña incorrecto",
                    IsSuccess = false,
                };

            var roles = await userManager.GetRolesAsync(user);

            if(!roles.Contains("Usuario"))
            {
                return new LoginRespone
                {
                    Message = "No tiene acceso",
                    IsSuccess = false,
                };
            }
            var claims = new List<Claim>()
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.Email, user.Id),

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginRespone
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                Ubication = user.Ubication,
                Phone = user.Phone,
                BirthDate = user.BirthDate,
                Email = user.Email,
                idUsuario = user.Id,
                name=user.Name,
                lastName=user.LastName,
                longitude = user.longitude,
                latitude = user.latitude
            };

        }

        public async Task<LoginRepartidorResponse> LoginRepartidorAsync(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new LoginRepartidorResponse
                {
                    Message = "Usuario o contraseña incorrecto",
                    IsSuccess = false,
                };
            }
            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new LoginRepartidorResponse
                {
                    Message = "Usuario o contraseña incorrecto",
                    IsSuccess = false,
                };

            var roles = await userManager.GetRolesAsync(user);

            if (!roles.Contains("Repartidor"))
            {
                return new LoginRepartidorResponse
                {
                    Message = "No tiene acceso!",
                    IsSuccess = false,
                };
            }
            var claims = new List<Claim>()
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.Email, user.Id),

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginRepartidorResponse
            {
                idRepartidor = user.Id,
                IsSuccess = true,
                Message = tokenAsString,
                ExpireDate = token.ValidTo,
                Name = user.Name,
                LastName = user.LastName,
                Ubication = user.Ubication,
                Phone = user.Phone,
                Email = user.Email
    };
        }






        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model, string rol)
        {
            if (model == null)
            {
                throw new NullReferenceException("model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "contraseñas no son iguales",
                    IsSuccess = false
                };
            }

            var identityUser = new UsuarioEntity
            {
                Email = model.Email,
                //UserName = model.Name.Replace(" ", string.Empty).Trim(),
                UserName = model.Email,
                Ubication = model.Ubication,
                Phone = model.Phone,
                BirthDate = model.BirthDate,
                latitude = "NoTiene",
                longitude = "NoTiene",
                Name = model.Name,
                LastName = model.LastName
                
                
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);
            
            if (result.Succeeded)
            {

                await userManager.AddToRoleAsync(identityUser, rol);
                return new UserManagerResponse
                {
                    Message = "Usuario creado exitosamente",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Usuario no fue creado",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        public async Task<UserManagerResponse> CreateRolAsync(CreateRoleViewModel model)
        {
            var identityRole = new IdentityRole()
            {
                Name = model.Name,
                NormalizedName = model.NormalizedName
            };
            var result = await roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Rol creado exitosamente",
                    IsSuccess = true
                };

            }

            return new UserManagerResponse
            {
                Message = "Role no fue creado",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        public async Task<UserManagerResponse> CreateUserRoleAsync(CreateUserRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return new UserManagerResponse
                {
                    Message = "Rol no existe",
                    IsSuccess = false
                };
            }
            var user = await userManager.FindByIdAsync(model.UserId);
            if (role == null)
            {
                return new UserManagerResponse
                {
                    Message = "Usuario no existe",
                    IsSuccess = false
                };
            }

            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                return new UserManagerResponse
                {
                    Message = "El usuario ya cuenta con un rol",
                    IsSuccess = false
                };
            }

            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "rol asignado exitosamente",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "algo salio mal",
                IsSuccess = false
            };
        }

    }
}
