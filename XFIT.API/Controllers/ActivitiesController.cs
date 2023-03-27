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

    [HttpPost("import")]
    public async Task<IActionResult> ImportActivities()
    {
        string path = "C:\\Users\\denni\\Downloads\\data-2023-3-22-21.csv";
        await _activityService.ImportActivitiesAsync(path);
        return Ok();
    }

}
