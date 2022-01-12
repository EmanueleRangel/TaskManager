using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Attributes;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;
using TaskManager.API.Services;
using TaskManager.API.Services.Usuarios;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginInputModel modelo)
        {
           return await _usuariosService.Authenticate(modelo);

        }

        // GET api/usuarios
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Usuario> Get()
        {
            return _usuariosService.Get();       
        }

        // GET api/usuarios/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public Usuario Get([FromRoute] string id)
        {
            return _usuariosService.Get(id);
        }

        // POST api/usuarios
        [HttpPost]
        [ApiKey]
        public IActionResult Post([FromBody] UsuarioInputModel novoUsuario)
        {
            _usuariosService.Post(novoUsuario);
            return Created("", novoUsuario);
        }

        // DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
           _usuariosService.Delete(id);
            return NoContent();
        }
    }
}
