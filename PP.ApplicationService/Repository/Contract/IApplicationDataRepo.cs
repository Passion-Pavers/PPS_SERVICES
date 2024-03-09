using PP.ApplicationService.Models.DbEntities;

namespace PP.ApplicationService.Repository.Contract
{
    public interface IApplicationDataRepo
    {
        Task<IEnumerable<Application>> GetAllActiveApplicationsAsync();
    }
}