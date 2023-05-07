using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Educator.Api.Controllers
{
	[ApiController]
	[Route("api/replacement")]
	public class ReplacementController : ControllerBase
	{
		private readonly IReplacementLogic _logic;
		public ReplacementController(IReplacementLogic logic)
		{
			_logic = logic;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReplacementDto>>> GetAvailableReplacements()
		{
			var result = await _logic.GetAvailableReplacements();
			if (result is null)
				return BadRequest($"B³¹d podczas pobierania dostêpnych zamian");
			return Ok(result);
		}

		[HttpGet("my/{userId}")]
		public async Task<ActionResult<IEnumerable<ReplacementDto>>> GetMyReplacements(int userId)
		{
			var result = await _logic.GetMyReplacements(userId);
			if (result == null)
				return NotFound("B³¹d podczas pobierania zamian u¿ytkownika");

			return Ok(result);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<ReplacementDto>> GetReplacement(int id)
		{
			var result = await _logic.GetReplacement(id);
			if (result is null)
				return BadRequest($"B³¹d podczas pobierania zamiany");
			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<IEnumerable<ReplacementDto>>> UpdateReplacement(UpdateReplacementParameters parameters)
		{
			var result = await _logic.UpdateReplacement(parameters);
			if (result is null)
				return BadRequest($"B³¹d podczas edycji zamiany");

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<ReplacementDto>> CreateReplacement(CreateReplacementParameters parameters)
		{
			ReplacementDto? result = await _logic.CreateReplacement(parameters);
			if (result is null)
				return BadRequest($"B³¹d podczas tworzenia zamiany");
			if(result.IsReplacementPossible == false)
				return BadRequest($"Zamiana niedozwolona dla wybranych zajêæ");
			if (result.OldStudent?.RoleName == "Teacher")
				return BadRequest($"Nauczyciel nie mo¿e dokonywaæ zamian miêdzy innymi uczniami");

			return Ok(result);
		}
	}
}