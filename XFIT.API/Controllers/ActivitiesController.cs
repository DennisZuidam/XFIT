using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XFIT.Core.Entities;
using XFIT.Core.Services;

namespace XFIT.API.Controllers;

[Route("api/activities")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Activity>> Get()
    {
        var activities = _activityService.GetAllActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public ActionResult<Activity> Get(int id)
    {
        var activity = _activityService.GetActivityById(id);

        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpPost]
    public ActionResult<Activity> Post(Activity activity)
    {
        _activityService.AddActivityAsync(activity);
        return CreatedAtAction(nameof(Get), new { id = activity.Id }, activity);
    }
    
    [HttpPost("import")]
    public async Task<IActionResult> ImportActivities([FromBody] string path)
    {
        await _activityService.ImportActivitiesAsync(path);
        return Ok();
    }

}
