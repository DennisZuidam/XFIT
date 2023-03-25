using System;

namespace XFIT.Core.Entities;

public class Activity
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public Athlete Athlete { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Distance { get; set; }
    public string Unit { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal? Elev { get; set; }
    public decimal? Calo { get; set; }
    public decimal EstPace { get; set; }
    public decimal EstSpeed { get; set; }
}

public enum ActivityType
{
    Run,
    Cycle,
    Swim
}