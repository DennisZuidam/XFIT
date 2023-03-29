using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using XFIT.Core.Entities;
using XFIT.Core.Services;

namespace XFIT.API.Controllers;

[Route("api/activities")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly string _csvFilePath;

    public ActivitiesController(IActivityService activityService, IConfiguration config)
    {
        _activityService = activityService;
        _csvFilePath = config["CsvFilePath"];
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
        await _activityService.ImportActivitiesAsync(_csvFilePath);
        return Ok();
    }

}
