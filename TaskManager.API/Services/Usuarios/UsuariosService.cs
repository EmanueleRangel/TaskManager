using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;

namespace TaskManager.API.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }
        public IEnumerable<Usuario> Get()
        {
            var usuarios = _usuariosRepository.Buscar();
            return usuarios;
        }
    }
}
