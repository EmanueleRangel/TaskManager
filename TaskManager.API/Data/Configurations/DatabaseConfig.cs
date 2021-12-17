namespace TaskManager.API.Data.Configurations
{
    public class DatabaseConfig : IDatabaseConfig
    {
       
        public string ConnectionStringSQL { get; set; }
        public string ConnectionStringMongo { get; set; }
    }
}
