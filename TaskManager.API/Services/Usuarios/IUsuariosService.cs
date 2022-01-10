using TaskManager.API.Models;

namespace TaskManager.API.Services.Usuarios
{
    public interface IUsuariosService
    {
        public IEnumerable<Usuario> Get();
    }
}
