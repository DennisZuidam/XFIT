using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using XFIT.Core.Entities;

namespace XFIT.Core.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<Activity> GetActivityById(int id);
        Task AddActivities(IEnumerable<Activity> activities);
        Task UpdateActivityAsync(Activity activityToBeUpdated, Activity activity);
        Task DeleteActivityAsync(Activity activity);
        Task ImportActivitiesAsync(string path);
    }
}