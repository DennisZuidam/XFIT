using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<IEnumerable<Activity>> ImportAsync(string filePath)
    {
        IEnumerable<Activity> activities = ReadCsvFile(filePath).ToList();

        await _activityRepository.AddAsync(activities);
        //_activityRepository.SaveChangesAsync();
        return activities;
    }


    private IEnumerable<Activity> ReadCsvFile(string path)
    {
        List<Activity> activities = new List<Activity>();
        
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var reader = new StreamReader(fileStream))
        {
            // Skip header line
            reader.ReadLine();
            while (reader.ReadLine() is { } line)
            {
                activities.AddRange(ParseCsvLine(line));
            }
        }

        return activities;
    }
    
    private Activity[] ParseCsvLine(string line)
    {
        List<Activity> activities = new List<Activity>();
        List<string> values = new List<string>();
        bool inQuote = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '\"')
            {
                inQuote = !inQuote;
            }
            else if (c == ',' && !inQuote)
            {
                values.Add(string.Empty);
            }
            else
            {
                if (values.Count == 0)
                {
                    values.Add(string.Empty);
                }
                values[values.Count - 1] += c;
            }
        }

        if (values.Count == 0)
        {
            return new Activity[0];
        }

        Activity activity = new Activity
        {
            Athlete = values[0],
            ActivityId = values[1],
            Type = values[2],
            Location = values[3],
            Name = values[4],
            Date = DateTime.ParseExact(values[5].Split('T')[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
            Distance = string.IsNullOrEmpty(values[6]) ? 0 : decimal.Parse(values[6]),
            Pace = string.IsNullOrEmpty(values[7]) ? TimeSpan.Zero : TimeSpan.Parse(values[7]),
            Unit = values[8],
            Duration = string.IsNullOrEmpty(values[9]) ? TimeSpan.Zero : TimeSpan.Parse(values[9]),
            Elev = string.IsNullOrEmpty(values[10]) ? 0 : decimal.Parse(values[10]),
            Calo = string.IsNullOrEmpty(values[11]) ? 0 : decimal.Parse(values[11]),
            EstPace = values[12],
            EstSpeed = string.IsNullOrEmpty(values[13]) ? 0 : decimal.Parse(values[13])
        };
        activities.Add(activity);

        return activities.ToArray();
    }

}
