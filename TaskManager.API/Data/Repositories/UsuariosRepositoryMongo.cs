using TaskManager.API.Data.Configurations;
using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public class UsuariosRepositoryMongo : RepositoryMongo<Usuario>, IUsuariosRepository
    {
        public UsuariosRepositoryMongo(IDatabaseConfig configuration) : base(configuration) { }

        public void Adicionar(Usuario usuario)
        {
            base.Adicionar(usuario);
        }

        public void Remover(Usuario usuario)
        {
            base.Remover(usuario.Id);
        }

        IEnumerable<Usuario> IUsuariosRepository.Buscar()
        {
            return base.Buscar();
        }

        Usuario IUsuariosRepository.Buscar(string id)
        {
            return base.Buscar(id);
        }
    }
}
