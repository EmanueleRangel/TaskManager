using Dapper.Contrib.Extensions;
using TaskManager.API.Models.Persist;

namespace TaskManager.API.Models
{
    [Table("TaskManagerUser")]
    public class Usuario : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Usuario() {}

        public Usuario(string nome, string senha, string role)
        {
            Id = Guid.NewGuid().ToString();
            Name = nome;
            Password = senha;
            Role = role;
        }
    }


}
