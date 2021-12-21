using MongoDB.Driver;
using TaskManager.API.Data.Configurations;


namespace TaskManager.API.Data.Repositories
{
    public abstract class RepositoryMongo<T>
    {
        private readonly IMongoCollection<T> _connection;

        public RepositoryMongo(IDatabaseConfig configuration)
        {

            var client = new MongoClient(configuration.ConnectionStringMongo);

            var databaseName = client.ListDatabaseNames().FirstOrDefault();

            var database = client.GetDatabase(databaseName);

            _connection = database.GetCollection<T>(nameof(T));
        }

        public void Adicionar (T entity) 
        {
            _connection.InsertOne(entity);
        }

        public void Atualizar (string id, T entityAtualizado) 
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            _connection.ReplaceOne(filter, entityAtualizado);
        }

        public IEnumerable<T> Buscar() 
        {
            return _connection.Find(entity => true).ToList();

        }

        public T Buscar(string id) 
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return _connection.Find(filter).FirstOrDefault();
        }

        public void Remover(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            _connection.DeleteOne(filter);
        }
    }
}
