
using Dapper.Contrib.Extensions;
using TaskManager.API.Models.Persist;

namespace TaskManager.API.Models
{
    [Table("Task")]
    public class Tarefa : Entity
    {

        public string Id { get; set; }
        public string Name { get; private set; }
        public string Details { get; private set; }
        public bool Done { get; private set; }
        public DateTime DateRegistration { get; private set; }
        public DateTime? CompletionDate { get; private set; }
        
        public Tarefa() {}
        public void AtualizarTarefa(string nome, string detalhes, bool? concluido = false)
        {
            Name = nome;
            Details = detalhes;
            Done = concluido ?? false;
            CompletionDate = Done ? DateTime.Now : null;
        }

        public Tarefa(string nome, string detalhes)
        {
            Id = Guid.NewGuid().ToString();
            Name = nome;
            Details = detalhes;
            Done = false;
            DateRegistration = DateTime.Now;
            CompletionDate = null;

        }


    }
}
