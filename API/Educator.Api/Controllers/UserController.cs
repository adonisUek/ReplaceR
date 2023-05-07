using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Educator.Api.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UserController : ControllerBase
	{
		private readonly IUserLogic _logic;
		public UserController(IUserLogic logic)
		{
			_logic = logic;
		}

		[HttpGet("auth")]
		public async Task<ActionResult<UserAuthDto>> AuthenticateUser(string login, string password)
		{
			var user = await _logic.GetUser(login);
			if (user == null)
				return NotFound("Nie istnieje aktywny u�ytkownik o takim loginie");

			if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
				return BadRequest("Niepoprawne has�o");

			return Ok(user);
		}

		[HttpGet]
		public async Task<ActionResult<List<UserDto>>> GetUsers()
		{
			var users = await _logic.GetUsers();
			if (users == null)
				return NotFound("B��d podczas pobierania u�ytkownik�w");

			return Ok(users);
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(CreateUserParameters parameters)
		{
			UserAuthDto? user = await _logic.GetUser(parameters.Login);
			if(user != null)
				return BadRequest($"Istnieje ju� u�ytkownik o loginie {parameters.Login}");
			
			var result = await _logic.CreateUser(parameters);
			if (result is null)
				return BadRequest($"U�ytkownik dodany niepoprawnie");
			
			return Ok(result);
		}
		[HttpPut]
		public async Task<ActionResult<IEnumerable<User>>> UpdateUser(int id, UpdateUserParameters parameters)
		{
			var result = await _logic.UpdateUser(id, parameters);
			if (result is null)
				return BadRequest($"Nie istnieje u�ytkownik o id {id}");

			return Ok(result);
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteUser(int id)
		{
			var result = await _logic.DeleteUser(id);
			if(result == false)
				return BadRequest($"Nie uda�o si� usun�� u�ytkownika o id {id}");

			return Ok(result);
		}
	}
}