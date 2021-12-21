using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;


namespace TaskManager.API.Data.Repositories
{
    
    public class TarefasRepositorySQL : RepositorySQL, ITarefasRepository
    {
        public TarefasRepositorySQL(IDatabaseConfig configuration) : base(configuration){}

        public void Adicionar(Tarefa tarefa)
        {
            base.Adicionar<Tarefa>(tarefa);
        }

        public void Atualizar(Tarefa tarefaAtualizada)
        {
            base.Atualizar<Tarefa>(tarefaAtualizada);
        }

        public void Remover(Tarefa tarefa)
        {
            base.Remover<Tarefa>(tarefa);
        }

        IEnumerable<Tarefa> ITarefasRepository.Buscar()
        {
           return base.Buscar<Tarefa>();
        }

        Tarefa ITarefasRepository.Buscar(string id)
        {
            return base.Buscar<Tarefa>(id);
        }
    }
}
