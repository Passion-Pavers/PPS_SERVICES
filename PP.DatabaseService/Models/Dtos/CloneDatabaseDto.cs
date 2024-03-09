namespace PP.DatabaseService.Models.Dtos
{
    public class CloneDatabaseDto
    {
        public string ExistingDatabaseConnectionString { get; set; }
        public string NewDatabaseName { get; set; }
    }
}
