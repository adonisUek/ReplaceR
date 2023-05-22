using Replacer.Api.Logic.Interfaces;
using Replacer.Api.Logic.Parameters;
using Replacer.DbModels;
using Replacer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Replacer.Api.Controllers
{
	[Route("api/activity")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IActivityLogic _logic;
		public ActivityController(IActivityLogic logic)
		{
			_logic = logic;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Activity>>> GetActivities(int userId)
		{
			var result = await _logic.GetActivities(userId);
			if (result is null)
				return BadRequest($"Błąd podczas pobierania aktywności");
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Activity>> GetActivity(int id)
		{
			var result = await _logic.GetActivity(id);
			if (result is null)
				return BadRequest($"Błąd podczas pobierania aktywności");
			return Ok(result);
		}

		[HttpGet("my/{userId}")]
		public async Task<ActionResult<IEnumerable<ActivityDto>>> GetMyActivities(int userId)
		{
			var result = await _logic.GetMyActivities(userId);
			if (result == null)
				return NotFound("Błąd podczas pobierania aktywności użytkownika");

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Activity>>> CreateActivity(CreateActivityParameters parameters)
		{
			var result = await _logic.CreateActivity(parameters);
			if (result is null)
				return BadRequest($"Błąd podczas tworzenia aktywności");
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<IEnumerable<ActivityDto>>> UpdateActivity(int id, UpdateActivityParameters parameters)
		{
			var result = await _logic.UpdateActivity(id, parameters);
			if (result is null)
				return BadRequest($"Błąd podczas edycji aktywności");

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteActivity(int id)
		{
			var result = await _logic.DeleteActivity(id);
			if (result == false)
				return BadRequest($"Błąd podczas usuwania aktywności");
			return Ok(result);
		}
	}
}