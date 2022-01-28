using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult<dynamic>> Authenticate(LoginInputModel modelo)
        {
            var usuario = _usuariosRepository.BuscarUsuarioPorNome(modelo.Nome);
             
            bool senhaValida = BCrypt.Net.BCrypt.Verify(modelo.Senha, usuario.Senha);

            if (!senhaValida)
                throw new Exception("Usuário ou senha inválidos");

            var token = TokenService.GenerateToken(usuario);

            usuario.Senha = "";

            return new
            {
                usuario = usuario,
                token = token
            };
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

        public bool Post(UsuarioInputModel novoUsuario)
        {
            novoUsuario.Senha = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);
            var usuario = new Usuario(novoUsuario.Nome, novoUsuario.Senha, novoUsuario.Role);

            _usuariosRepository.Adicionar(usuario);

            return true;
        }
    }
}
