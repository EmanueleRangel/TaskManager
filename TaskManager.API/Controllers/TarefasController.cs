using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Attributes;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;
using TaskManager.API.Services.Tarefas;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {

        private ITarefasService _tarefasService;


        public TarefasController(ITarefasService tarefasService)
        {
            _tarefasService = tarefasService;

        }

        // GET api/tarefas
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Tarefa> Get()
        {
           return _tarefasService.Get();
        }
                 

        // GET api/tarefas/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public Tarefa Get([FromRoute] string id)
        {
            return _tarefasService.Get(id);

        }

        // POST api/tarefas
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] TarefaInputModel novaTarefa)
        {
           _tarefasService.Post(novaTarefa);
            return Created("", novaTarefa);
        }

        // PUT api/tarefas/{id}
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put([FromRoute] string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            _tarefasService.Put(id, tarefaAtualizada);
            return Ok(tarefaAtualizada);
        }

        // DELETE api/tarefas/{id}
        [HttpDelete("{id}")]
        [Authorize (Roles = "gerente")]
        public IActionResult Delete([FromRoute] string id)
        {
            _tarefasService.Delete(id);
           return NoContent();
        }
    }
}
