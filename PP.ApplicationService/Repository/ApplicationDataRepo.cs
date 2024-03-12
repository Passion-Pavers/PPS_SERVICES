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

        public async Task AddApplicationAsync(Application application)
        {
            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<string> GetAppConfigJson(int appID, int subAppID)
        {
            string? appConfig = string.Empty;

            if (subAppID != 0)
            {
                appConfig =await _dbContext.SubApps
                    .Where(s => s.SubAppID == subAppID)
                    .Select(s => s.AppConfigJson)
                    .FirstOrDefaultAsync();
            }

            if (string.IsNullOrEmpty(appConfig))
            {
                // If SubApp not found, get AppConfigJSON from parent Application
                appConfig = await _dbContext.Applications
                    .Where(a => a.Id == appID)
                    .Select(a => a.AppConfigJson)
                    .FirstOrDefaultAsync();
            }

            return appConfig;
        }
    }
}
