using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;

namespace Educator.Api.Logic.Interfaces
{
    public interface IUserLogic
    {
        Task<IEnumerable<UserDto>?> GetUsers();
		Task<UserAuthDto?> GetUser(string login);
		Task<User?> CreateUser(CreateUserParameters parameters);
		Task<User?> UpdateUser(int id, UpdateUserParameters parameters);
		Task<bool> DeleteUser(int id);
	}
}