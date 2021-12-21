using Dapper.Contrib.Extensions;
using TaskManager.API.Models.Persist;

namespace TaskManager.API.Models
{
    [Table("Usuario")]
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
