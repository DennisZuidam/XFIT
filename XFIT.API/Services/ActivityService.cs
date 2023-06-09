﻿using System.Collections.Generic;
using System.Threading.Tasks;
using XFIT.Core.Entities;
using XFIT.Core.Repositories;
using XFIT.Core.Services;

namespace XFIT.Api.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IActivityImporter _activityImporter;

        public ActivityService(IActivityRepository activityRepository, IActivityImporter activityImporter)
        {
            _activityRepository = activityRepository;
            _activityImporter = activityImporter;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _activityRepository.GetAllAsync();
        }

        public async Task<Activity> GetActivityById(int id)
        {
            return await _activityRepository.GetByIdAsync(id);
        }

        public async Task AddActivities(IEnumerable<Activity> activities)
        {
            await _activityRepository.AddAsync(activities);
        }

        public async Task UpdateActivityAsync(Activity activityToBeUpdated, Activity activity)
        {
            activityToBeUpdated.Athlete = activity.Athlete;
            activityToBeUpdated.ActivityId = activity.ActivityId;
            activityToBeUpdated.Type = activity.Type;
            activityToBeUpdated.Location = activity.Location;
            activityToBeUpdated.Name = activity.Name;
            activityToBeUpdated.Date = activity.Date;
            activityToBeUpdated.Distance = activity.Distance;
            activityToBeUpdated.Pace = activity.Pace;
            activityToBeUpdated.Unit = activity.Unit;
            activityToBeUpdated.Duration = activity.Duration;
            activityToBeUpdated.Elev = activity.Elev;
            activityToBeUpdated.Calo = activity.Calo;
            activityToBeUpdated.EstPace = activity.EstPace;
            activityToBeUpdated.EstSpeed = activity.EstSpeed;

            await _activityRepository.Update(activityToBeUpdated);
            await _activityRepository.SaveChangesAsync();
        }
        
        public async Task DeleteActivityAsync(Activity activity)
        {
            await _activityRepository.Delete(activity);
            await _activityRepository.SaveChangesAsync();
        }
        
        public async Task ImportActivitiesAsync(string path)
        {
            var activities = _activityImporter.ImportAsync(path).Result;
            await AddActivities(activities);
        }

    }
}