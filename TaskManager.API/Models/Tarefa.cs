
using Dapper.Contrib.Extensions;
using TaskManager.API.Models.Persist;

namespace TaskManager.API.Models
{
    [Table("Tarefa")]
    public class Tarefa : Entity
    {
        
        public string Nome { get; private set; }
        public string Detalhes { get; private set; }
        public bool Concluido { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        
        public Tarefa() {}
        public void AtualizarTarefa(string nome, string detalhes, bool? concluido = false)
        {
            Nome = nome;
            Detalhes = detalhes;
            Concluido = concluido ?? false;
            DataConclusao = Concluido ? DateTime.Now : null;
        }

        public Tarefa(string nome, string detalhes)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Detalhes = detalhes;
            Concluido = false;
            DataCadastro = DateTime.Now;
            DataConclusao = null;

        }


    }
}
