using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Models.Dtos;

namespace PP.CREDStroreService.Repository.Contract
{
    public interface ICredDataStoreRepo
    {
        Task AddAsync(Credentials Credentials);
        Task DeleteAsync(int id);
        Task<IEnumerable<Credentials>> GetAllAsync(GetCredentialsDto request);
        Task<Credentials> GetByIdAsync(int id);
        Task UpdateAsync(Credentials Credentials);
    }
}