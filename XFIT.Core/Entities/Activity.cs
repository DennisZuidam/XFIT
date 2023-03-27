using System;

namespace XFIT.Core.Entities;

public class Activity
{
    public int Id { get; set; }
    public string Athlete { get; set; }
    public string ActivityId { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Distance { get; set; }
    public TimeSpan Pace { get; set; }
    public string Unit { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal Elev { get; set; }
    public decimal Calo { get; set; }
    public string EstPace { get; set; }
    public decimal EstSpeed { get; set; }
}


public enum ActivityType
{
    Run,
    Cycle,
    Swim
}