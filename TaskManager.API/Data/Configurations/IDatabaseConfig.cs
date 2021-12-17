namespace TaskManager.API.Data.Configurations
{
    public interface IDatabaseConfig
    {
        string ConnectionStringSQL { get; set; }
        string ConnectionStringMongo { get; set; }
        

    }
}
