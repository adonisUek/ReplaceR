using Educator.Api.Logic.Interfaces;
using Educator.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Educator.Api.Controllers
{
	[Route("api/subject")]
	[ApiController]
	public class SubjectController : ControllerBase
	{
		private readonly ISubjectLogic _logic;
		public SubjectController(ISubjectLogic logic)
		{
			_logic = logic;
		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<SubjectDto>?>> GetSubjects()
		{
			var result = await _logic.GetSubjects();
			if (result is null)
				return BadRequest($"Błąd podczas pobierania przedmiotów");
			return Ok(result);
		}
	}
}