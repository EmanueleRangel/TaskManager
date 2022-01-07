using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Services.Tarefas
{
    public interface ITarefasService
    {
        public IEnumerable<Tarefa> Get();

        public Tarefa Get(string id);

        public void Post(TarefaInputModel novaTarefa);

        public void Put(string id, TarefaInputModel tarefaAtualizada);

        public void Delete(string id);
    }
}
