using Microsoft.EntityFrameworkCore;
using PP.SampleCRUDService.Data;
using PP.SampleCRUDService.Models.DbEntities;
using PP.SampleCRUDService.Repository.Contract;

namespace PP.SampleCRUDService.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public ApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _dbContext.Applications.ToListAsync();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _dbContext.Applications.FindAsync(id);
        }

        public async Task AddAsync(Application application)
        {
            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Application application)
        {
            _dbContext.Entry(application).State = EntityState.Modified;
            _dbContext.Entry(application).Property(e => e.CreatedOn).IsModified = false;
            _dbContext.Entry(application).Property(e => e.CreatedBy).IsModified = false;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var application = await _dbContext.Applications.FindAsync(id);
            if (application != null)
            {
                _dbContext.Applications.Remove(application);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
