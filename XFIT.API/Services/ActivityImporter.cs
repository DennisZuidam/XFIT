using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XFIT.Core.Entities;
using XFIT.Core.Repositories;
using XFIT.Core.Services;

namespace XFIT.Api.Services;

public class ActivityImporter : IActivityImporter
{
    private readonly IActivityRepository _activityRepository;

    public ActivityImporter(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public IEnumerable<Activity> Import(string filePath)
    {
        IEnumerable<Activity> activities = ReadCsvFile(filePath).ToList();

        foreach (Activity activity in activities)
        {
            _activityRepository.Add(activity);
        }

        _activityRepository.SaveChangesAsync();
        return activities;
    }


    private IEnumerable<Activity> ReadCsvFile(string path)
    {
        List<Activity> activities = new List<Activity>();

        using StreamReader reader = new StreamReader(path);

        while (reader.ReadLine() is { } line)
        {
            string[] values = line.Split(',');

            Activity activity = new Activity
            {
                Athlete = values[0],
                ActivityId = values[1],
                Type = values[2],
                Location = values[3],
                Name = values[4],
                Date = DateTime.Parse(values[5]),
                Distance = decimal.Parse(values[6]),
                Pace = TimeSpan.Parse(values[7]),
                Unit = values[8],
                Duration = TimeSpan.Parse(values[9]),
                Elev = string.IsNullOrEmpty(values[10]) ? 0 : decimal.Parse(values[10]),
                Calo = string.IsNullOrEmpty(values[11]) ? 0 : decimal.Parse(values[11]),
                EstPace = TimeSpan.Parse(values[12]),
                EstSpeed = decimal.Parse(values[13])
            };

            activities.Add(activity);
        }

        return activities;
    }

}
