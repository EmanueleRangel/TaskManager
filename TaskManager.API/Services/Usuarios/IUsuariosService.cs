using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Services.Usuarios
{
    public interface IUsuariosService
    {
        public IEnumerable<Usuario> Get();

        public void Post(UsuarioInputModel novoUsuario);

        public Usuario Get(string id);

        void Delete(string id);

        public Task<ActionResult<dynamic>> Authenticate(LoginInputModel modelo);

    }
}
