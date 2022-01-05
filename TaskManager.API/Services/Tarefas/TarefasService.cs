using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;

namespace TaskManager.API.Services.Tarefas
{
    public class TarefasService : ITarefasService
    {
        private readonly ITarefasRepository _tarefasRepository;

        public Task<Tarefa> Get()
        {
            var tarefas = _tarefasRepository.Buscar();
            return (Task<Tarefa>)tarefas;
        }
    }
}
