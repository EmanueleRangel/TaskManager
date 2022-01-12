using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Services.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public void Delete(string id)
        {
            var usuario = _usuariosRepository.Buscar(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            _usuariosRepository.Remover(usuario);
        }

        public IEnumerable<Usuario> Get()
        {
            var usuarios = _usuariosRepository.Buscar();
            return usuarios;
        }

        public Usuario Get(string id)
        {
            var usuario = _usuariosRepository.Buscar(id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            return usuario;
        }

        public void Post(UsuarioInputModel novoUsuario)
        {
            novoUsuario.Senha = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);
            var usuario = new Usuario(novoUsuario.Nome, novoUsuario.Senha, novoUsuario.Role);

            _usuariosRepository.Adicionar(usuario);
        }
    }
}
