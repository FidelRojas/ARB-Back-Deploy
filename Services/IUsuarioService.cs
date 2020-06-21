using ARB.Models;
using ARB.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Services
{
    public interface IUsuarioService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model, string rol);
        Task<LoginRespone> LoginUserAsync(LoginViewModel model);
        Task<LoginRepartidorResponse> LoginRepartidorAsync(LoginViewModel model);
        Task<UserManagerResponse> CreateRolAsync(CreateRoleViewModel model);
        Task<UserManagerResponse> CreateUserRoleAsync(CreateUserRoleViewModel model);

        Task<IEnumerable<UserRespons>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioAsync(string id);
        Task<bool> DeleteUsuarioAsync(string id);
        Task<EditUsuarioResponse> UpdateUsuarioAsync(string id, Usuario usuario);

    }
}

