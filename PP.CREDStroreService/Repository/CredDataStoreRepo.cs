using Microsoft.EntityFrameworkCore;
using PP.CREDStroreService.Data;
using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Models.Dtos;
using PP.CREDStroreService.Repository.Contract;
using System.Linq.Expressions;

namespace PP.CREDStroreService.Repository
{
    public class CredDataStoreRepo : ICredDataStoreRepo
    {
        private readonly AppDbContext _dbContext;

        public CredDataStoreRepo(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Credentials>> GetAllAsync(GetCredentialsDto request)
        {
            if (request.Id == 0)
                return await _dbContext.Credentials.ToListAsync();
            else
            {
                var credential = await _dbContext.Credentials.FindAsync(request.Id);
                return new List<Credentials> { credential };    
            }

        }

        public async Task<Credentials> GetByIdAsync(int id)
        {
            return await _dbContext.Credentials.FindAsync(id);
        }

        public async Task AddAsync(Credentials Credentials)
        {
            _dbContext.Credentials.Add(Credentials);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Credentials Credentials)
        {
            _dbContext.Entry(Credentials).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Credentials = await _dbContext.Credentials.FindAsync(id);
            if (Credentials != null)
            {
                _dbContext.Credentials.Remove(Credentials);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
