using System.Collections.Generic;
using System.Threading.Tasks;
using XFIT.Core.Entities;

namespace XFIT.Core.Repositories;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetAllAsync();
    Task<Activity> GetByIdAsync(int id);
    Task Add(Activity activity);
    Task Update(Activity activity);
    Task Delete(Activity activity);
    Task SaveChangesAsync();
}

