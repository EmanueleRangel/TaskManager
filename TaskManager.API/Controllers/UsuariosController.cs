using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

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

        // GET api/usuarios
        [HttpGet]
        public IActionResult Get()
        {
            var usuarios = _usuariosRepository.Buscar();
            return Ok(usuarios);
        }

        // GET api/usuarios/{id}
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            var usuario = _usuariosRepository.Buscar(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // POST api/usuarios
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioInputModel novoUsuario)
        {
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
