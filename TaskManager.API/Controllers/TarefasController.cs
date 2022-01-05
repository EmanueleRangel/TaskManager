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
        private ITarefasRepository _tarefasRepository;
        

        //construtor passando por uma injeçao de dependencia
        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;

        }
        // comentario parte 1

        // GET api/tarefas
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Tarefa>> Get()
            => await _tarefas.Get();
     

        // GET api/tarefas/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if(tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        // POST api/tarefas
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] TarefaInputModel novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);

            _tarefasRepository.Adicionar(tarefa);

            return Created("", tarefa);
        }

        // PUT api/tarefas/{id}
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put([FromRoute] string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                return NotFound();

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);

            _tarefasRepository.Atualizar(tarefa);

            return Ok(tarefa);
        }

        // DELETE api/tarefas/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete([FromRoute] string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                return NotFound();

            _tarefasRepository.Remover(tarefa);

            return NoContent();

            //comentario parte 2
        }
    }
}
