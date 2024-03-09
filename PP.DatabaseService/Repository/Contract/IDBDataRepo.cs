using PP.DatabaseService.Models.Dtos;

namespace PP.DatabaseService.Repository.Contract
{
    public interface IDBDataRepo
    {
        ResponseDto CloneDatabase(string existingConnectionString, string newDatabaseName);
    }
}