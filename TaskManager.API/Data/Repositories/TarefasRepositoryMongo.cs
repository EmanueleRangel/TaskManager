using MongoDB.Driver;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public class TarefasRepositoryMongo : ITarefasRepository
    {
        //configuraçao das tarefas no mongo
        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefasRepositoryMongo(IDatabaseConfig databaseConfig )
        {
           //iniciaçao do objeto que recebe a collection
            var client = new MongoClient(databaseConfig.ConnectionStringMongo);

            var databaseName = client.ListDatabaseNames().FirstOrDefault(); 
            
            var database = client.GetDatabase(databaseName);

            _tarefas = database.GetCollection<Tarefa>("tarefas");
        }

        public void Adicionar(Tarefa tarefa)
        {
            _tarefas.InsertOne(tarefa);
        }

        public void Atualizar(Tarefa tarefaAtualizada)
        {
            _tarefas.ReplaceOne(tarefa => tarefa.Id == tarefaAtualizada.Id, tarefaAtualizada);
        }

        public IEnumerable<Tarefa> Buscar()
        {
            return _tarefas.Find(tarefa => true).ToList();
            
        }

        public Tarefa Buscar(string id)
        {
            return _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefault();
        }

        public void Remover(Tarefa tarefa)
        {
            _tarefas.DeleteOne(t => t.Id == tarefa.Id);
        }
    }
}
