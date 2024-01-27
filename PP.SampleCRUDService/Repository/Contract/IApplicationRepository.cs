using PP.SampleCRUDService.Models.DbEntities;

namespace PP.SampleCRUDService.Repository.Contract
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> GetByIdAsync(int id);
        Task AddAsync(Application application);
        Task UpdateAsync(Application application);
        Task DeleteAsync(int id);
    }
}
