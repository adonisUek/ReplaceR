using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Educator.Api.Controllers
{
	[Route("api/enrollment")]
	[ApiController]
	public class EnrollmentController : ControllerBase
	{
		private readonly IEnrollmentLogic _logic;
		public EnrollmentController(IEnrollmentLogic logic)
		{
			_logic = logic;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments(int userId, bool withReplacementOnly)
		{
			var result = await _logic.GetEnrollments(userId, withReplacementOnly);
			if (result is null)
				return BadRequest($"Błąd podczas pobierania zapisów");
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
		{
			var result = await _logic.GetEnrollment(id);
			if (result is null)
				return BadRequest($"Błąd podczas pobierania zapisu");
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<IEnumerable<Enrollment>>> CreateEnrollments(CreateEnrollmentParameters[] parameters)
		{
			List<EnrollmentDto>? enrollments = (List<EnrollmentDto>?)await _logic.GetEnrollments(parameters[0].StudentId, false);
			int[]? alreadyAssigned = enrollments?.Select(e => e.SubjectDetailId).ToList().Intersect(parameters.Select(param => param.SubjectDetailId).ToList()).ToArray();
			if (enrollments != null && alreadyAssigned?.Length > 0)
				return BadRequest($"Jesteś już zapisany na przedmiot {alreadyAssigned?[0]}");
			var result = await _logic.CreateEnrollments(parameters);
			if (result is null)
				return BadRequest($"Błąd podczas tworzenia zapisu");
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteEnrollment(int id)
		{
			var result = await _logic.DeleteEnrollment(id);
			if (result == false)
				return BadRequest($"Błąd podczas usuwania zapisu");
			return Ok(result);
		}
	}
}