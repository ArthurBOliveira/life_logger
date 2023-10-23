using life_logger_models;

using life_logger_repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace life_logger_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepo _activityRepo;

        public ActivityController(IActivityRepo activityRepo)
        {
            _activityRepo = activityRepo;
        }

        [HttpPost]
        public ActionResult<Activity> CreateActivity(Activity activity)
        {
            var createdActivity = _activityRepo.Create(activity);
            return CreatedAtAction(nameof(GetActivity), new { id = createdActivity.Id }, createdActivity);
        }

        [HttpGet("{id}")]
        public ActionResult<Activity> GetActivity(Guid id)
        {
            var activity = _activityRepo.Select(id);
            if (activity == null)
            {
                return NotFound();
            }
            return activity;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Activity>> GetActivities()
        {
            var activities = _activityRepo.SelectMany();
            return Ok(activities);
        }

        [HttpGet("filter")]
        public ActionResult<IEnumerable<Activity>> GetFilteredActivities(string filter)
        {
            var activities = _activityRepo.SelectFilter(filter);
            return Ok(activities);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActivity(Guid id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            var existingActivity = _activityRepo.Select(id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            _activityRepo.Update(activity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActivity(Guid id)
        {
            var existingActivity = _activityRepo.Select(id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            _activityRepo.Delete(id);
            return NoContent();
        }
    }
}
