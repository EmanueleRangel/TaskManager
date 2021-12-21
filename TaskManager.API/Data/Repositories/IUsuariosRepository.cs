using TaskManager.API.Models;

namespace TaskManager.API.Data.Repositories
{
    public interface IUsuariosRepository
    {
        void Adicionar(Usuario usuario);

        IEnumerable<Usuario> Buscar();

        Usuario Buscar(string id);

        void Remover(Usuario usuario);
    }
}
