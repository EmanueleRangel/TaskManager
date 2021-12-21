using MongoDB.Driver;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public class TarefasRepositoryMongo : RepositoryMongo<Tarefa>, ITarefasRepository
    {
        public TarefasRepositoryMongo(IDatabaseConfig configuration) : base(configuration) { }
        public void Adicionar(Tarefa tarefa)
        {
            base.Adicionar(tarefa);
        }

        public void Atualizar(Tarefa tarefaAtualizada)
        {
            base.Atualizar(tarefaAtualizada.Id, tarefaAtualizada);
        }

        public void Remover(Tarefa tarefa)
        {
            base.Remover(tarefa.Id);
        }

        IEnumerable<Tarefa> ITarefasRepository.Buscar()
        {
           return base.Buscar();
        }

        Tarefa ITarefasRepository.Buscar(string id)
        {
            return base.Buscar(id);
        }
    }
}
