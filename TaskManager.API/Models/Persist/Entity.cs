using Dapper.Contrib.Extensions;

namespace TaskManager.API.Models.Persist
{
    public abstract class Entity
    {
        [ExplicitKey]
        public string Id { get; set; }

    }
}
