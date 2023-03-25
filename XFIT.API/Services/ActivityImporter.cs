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
                AthleteId = int.Parse(values[0].Split('/').Last()),
                Type = values[2],
                Name = values[4],
                Date = DateTime.Parse(values[5]),
                Distance = decimal.Parse(values[6]),
                Unit = values[8],
                Duration = TimeSpan.Parse(values[9]),
                Elev = decimal.TryParse(values[10], out decimal elev) ? elev : null,
                Calo = decimal.TryParse(values[11], out decimal calo) ? calo : null,
                EstPace = decimal.Parse(values[12]),
                EstSpeed = decimal.Parse(values[13])
            };

            activities.Add(activity);
        }

        return activities;
    }
}
