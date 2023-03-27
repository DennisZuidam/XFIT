using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XFIT.Core.Entities;
using XFIT.Core.Repositories;
using XFIT.Infrastructure.Data;

namespace XFIT.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly XfitDbContext _dbContext;

        public ActivityRepository(XfitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            using (_dbContext)
            {
                return await _dbContext.Activities.ToListAsync();
            }
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            using (_dbContext)
            {
                return await _dbContext.Activities.FindAsync(id);
            }
        }

        public void Add(IEnumerable<Activity> activities)
        {
            _dbContext.Activities.AddRange(activities);
        }

        public async Task Update(Activity activity)
        {
            using (_dbContext)
            {
                _dbContext.Activities.Update(activity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Activity activity)
        {
            using (_dbContext)
            {
                _dbContext.Activities.Remove(activity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}