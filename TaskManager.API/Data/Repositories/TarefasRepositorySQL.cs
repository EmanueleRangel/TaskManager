using Microsoft.Data.SqlClient;
using Dapper;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;
using Dapper.Contrib.Extensions;

namespace TaskManager.API.Data.Repositories
{
    
    public class TarefasRepositorySQL : ITarefasRepository, IDisposable
    {
        private SqlConnection _connection { get; set; }

        
        public TarefasRepositorySQL (IDatabaseConfig configuration)
        {
            _connection = new SqlConnection(configuration.ConnectionStringSQL);
            _connection.Open();
        }

      
        public void Adicionar(Tarefa tarefa)
        {
            _connection.Insert(tarefa);
        }

        public void Atualizar(Tarefa tarefaAtualizada)
        {
            _connection.Update(tarefaAtualizada);
        }

        public IEnumerable<Tarefa> Buscar()
        {
            return _connection.GetAll<Tarefa>();
        }

        public Tarefa Buscar(string id)
        {
            Tarefa tarefa = _connection.Get<Tarefa>(id);
            return tarefa;
        }

        public void Remover(Tarefa tarefa)
        {
            _connection.Delete(tarefa);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
