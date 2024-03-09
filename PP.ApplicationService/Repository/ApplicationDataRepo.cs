using Microsoft.EntityFrameworkCore;
using PP.ApplicationService.Data;
using PP.ApplicationService.Models.DbEntities;
using PP.ApplicationService.Repository.Contract;

namespace PP.ApplicationService.Repository
{
    public class ApplicationDataRepo : IApplicationDataRepo
    {
        private readonly AppDbContext _dbContext;

        public ApplicationDataRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Application>> GetAllActiveApplicationsAsync()
        {
            return await _dbContext.Applications.Where(x => x.IsActive == true).ToListAsync();
        }
    }
}
