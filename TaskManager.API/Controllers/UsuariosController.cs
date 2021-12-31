using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Attributes;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginInputModel modelo)
        {
            var usuario = _usuariosRepository.BuscarUsuarioPorNome(modelo.Nome);

            bool senhaValida = BCrypt.Net.BCrypt.Verify(modelo.Senha, usuario.Senha);
            
            if (!senhaValida) 
                return NotFound(new {message = "Usuário ou senha inválidos"});

            var token = TokenService.GenerateToken(usuario);

            usuario.Senha = "";

            return new
            {
                usuario = usuario,
                token = token
            };

        }

        // GET api/usuarios
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var usuarios = _usuariosRepository.Buscar();
            return Ok(usuarios);
        }

        // GET api/usuarios/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] string id)
        {
            var usuario = _usuariosRepository.Buscar(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // POST api/usuarios
        [HttpPost]
        [ApiKey]
        public IActionResult Post([FromBody] UsuarioInputModel novoUsuario)
        {
            novoUsuario.Senha = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);
            var usuario = new Usuario(novoUsuario.Nome, novoUsuario.Senha, novoUsuario.Role);

            _usuariosRepository.Adicionar(usuario);

            return Created("", usuario);
        }

        // DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var usuario= _usuariosRepository.Buscar(id);

            if (usuario == null)
                return NotFound();

            _usuariosRepository.Remover(usuario);

            return NoContent();
        }
    }
}
