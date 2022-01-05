using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;

namespace TaskManager.API.Services.Tarefas
{
    public interface ITarefasService
    {
        public Task<Tarefa> Get();
    }
}
