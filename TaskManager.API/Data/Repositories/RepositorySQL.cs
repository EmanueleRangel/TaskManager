using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using TaskManager.API.Data.Configurations;
using TaskManager.API.Models.Persist;

namespace TaskManager.API.Data.Repositories
{
    public abstract class RepositorySQL: IDisposable
    {
        protected SqlConnection _connection { get; set; }

        public RepositorySQL(IDatabaseConfig configuration)
        {
            _connection = new SqlConnection(configuration.ConnectionStringSQL);
            _connection.Open();
        }


        public void Adicionar<T>(T entity) where T : class
        {
            _connection.Insert(entity);
        }

        public void Atualizar<T>(T entity) where T : class
        {
            _connection.Update(entity);
        }

        public IEnumerable<T> Buscar<T>() where T : class
        {
            return _connection.GetAll<T>();
        }

        public T Buscar<T>(string id) where T : class
        {
            return _connection.Get<T>(id);
            
        }

        public void Remover<T>(T entity) where T : class
        {
            _connection.Delete(entity);
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
