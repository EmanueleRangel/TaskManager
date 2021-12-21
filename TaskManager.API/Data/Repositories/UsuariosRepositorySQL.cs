using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public class UsuariosRepositorySQL : RepositorySQL, IUsuariosRepository
    {
        public UsuariosRepositorySQL(IDatabaseConfig configuration) : base(configuration) {}

        public void Adicionar(Usuario usuario)
        {
            base.Adicionar<Usuario>(usuario);
        }

        public IEnumerable<Usuario> Buscar()
        {
            return base.Buscar<Usuario>();
        }

        public Usuario Buscar(string id)
        {
            return base.Buscar<Usuario>(id);
        }

        public void Remover(Usuario usuario)
        {
            base.Remover<Usuario>(usuario);
        }
    }
}
