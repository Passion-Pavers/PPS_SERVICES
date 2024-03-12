namespace PP.DatabaseService.Repository.Contract
{
    public interface IDataBaseService
    {
        Task CloneTables(string sourceConnectionString, string targetDatabaseName, string dblinkName);
        Task CreateDBLink(string sourceConnectionString, string targetDatabaseName, string dblinkName);
        Task CreateNewDatabase(string databaseName);
        Task<string[]> GetTableList(string sourceConnectionString);
    }
}