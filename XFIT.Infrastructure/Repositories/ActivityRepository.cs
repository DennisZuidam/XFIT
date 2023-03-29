using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            return await _dbContext.Activities.ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await _dbContext.Activities.FindAsync(id);
        }

        [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands")]
        public async Task AddAsync(IEnumerable<Activity> activities)
        {
            foreach (var activity in activities)
            {
                var existingActivity = await _dbContext.Activities.SingleOrDefaultAsync(a => a.ActivityId == activity.ActivityId);
                if (existingActivity == null)
                {
                    await _dbContext.Activities.AddAsync(activity);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Activity activity)
        {
            _dbContext.Activities.Update(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Activity activity)
        {
            _dbContext.Activities.Remove(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}