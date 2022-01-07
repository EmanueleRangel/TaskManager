using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Services.Tarefas
{
    public class TarefasService : ITarefasService
    {
        private ITarefasRepository _tarefasRepository;
        
        public TarefasService(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        public void Delete(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                throw new Exception("Tarefa não encontrada");

            _tarefasRepository.Remover(tarefa);
        }

        public IEnumerable<Tarefa> Get()
        {
            var tarefas =  _tarefasRepository.Buscar();
            return tarefas;
        }

        public Tarefa Get(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                throw new Exception("Tarefa não encontrada");

            return tarefa;
        }

        public void Post(TarefaInputModel novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);

            _tarefasRepository.Adicionar(tarefa);

        }

        public void Put(string id, TarefaInputModel tarefaAtualizada)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa == null)
                throw new Exception("Tarefa não encontrada");

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);

            _tarefasRepository.Atualizar(tarefa);
            
        }
    }
}
