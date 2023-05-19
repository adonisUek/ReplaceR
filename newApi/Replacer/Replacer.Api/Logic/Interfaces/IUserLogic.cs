using Replacer.Api.Logic.Parameters;
using Replacer.DbModels;
using Replacer.DTO;

namespace Replacer.Api.Logic.Interfaces
{
    public interface IUserLogic
    {
        Task<UserDto?> GetUser(int id);
		Task<UserAuthDto?> GetUser(string login);
		Task<User?> CreateUser(CreateUserParameters parameters);
		Task<User?> UpdateUser(int id, UpdateUserParameters parameters);
		Task<bool> DeleteUser(int id);
	}
}