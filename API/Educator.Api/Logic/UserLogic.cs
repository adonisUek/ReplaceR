using Educator.Api.Database;
using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace Educator.Api.Logic
{
	public class UserLogic : IUserLogic
	{
		private readonly EducatorDbContext _dbContext;
		public UserLogic(EducatorDbContext educatorDbContext)
		{
			_dbContext = educatorDbContext;
		}

		public async Task<User?> CreateUser(CreateUserParameters parameters)
		{
			try
			{
				User user = new()
				{
					Login = parameters.Login,
					PasswordHash = BCrypt.Net.BCrypt.HashPassword(parameters.Password),
					FirstName = parameters.FirstName,
					LastName = parameters.LastName,
					PhoneNumber = parameters.PhoneNumber,
					MailAddress = parameters.MailAddress,
					Address = parameters.Address,
					RoleId = parameters.RoleId,
					IsActive = true,
					IsEmailVerificationAllowed = parameters.IsEmailVerificationAllowed,
					IsSmsVerificationAllowed = parameters.IsSmsVerificationAllowed
				};

				_dbContext.Users.Add(user);
				await _dbContext.SaveChangesAsync();
				return user;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeleteUser(int id)
		{
			try
			{
				User? userToDelete = await _dbContext.Users.Where(user => user.Id == id).SingleOrDefaultAsync();
				if (userToDelete == null)
					return false;
				_dbContext.Users.Remove(userToDelete);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<UserAuthDto?> GetUser(string login)
		{
			try
			{
				var userWithLogin = await (from user in _dbContext.Users
										   where user.IsActive == true && user.Login == login
										   select new UserAuthDto()
										   {
											   Login = user.Login,
											   Password = user.PasswordHash
										   }).SingleOrDefaultAsync();
				return userWithLogin;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<UserDto>?> GetUsers()
		{
			try
			{
				var users = await (from user in _dbContext.Users
								   join role in _dbContext.Roles on user.RoleId equals role.Id
								   where user.IsActive == true
								   select new UserDto()
								   {
									   Id = user.Id,
									   Login = user.Login,
									   FirstName = user.FirstName,
									   LastName = user.LastName,
									   PhoneNumber = user.PhoneNumber,
									   MailAddress = user.MailAddress,
									   Address = user.Address,
									   RoleName = role.Name,
									   IsEmailVerificationAllowed = user.IsEmailVerificationAllowed,
									   IsSmsVerificationAllowed = user.IsSmsVerificationAllowed
								   }).ToListAsync();
				return users;
			}
			catch (Exception)
			{
				return null;
			}
		}


		public async Task<User?> UpdateUser(int id, UpdateUserParameters parameters)
		{
			try
			{
				User? userToEdit = await _dbContext.Users.FindAsync(id);
				if (userToEdit == null)
					return null;
				if (!string.IsNullOrEmpty(parameters.Password))
					userToEdit.PasswordHash = BCrypt.Net.BCrypt.HashPassword(parameters.Password);
				if (!string.IsNullOrEmpty(parameters.FirstName))
					userToEdit.FirstName = parameters.FirstName;
				if (!string.IsNullOrEmpty(parameters.LastName))
					userToEdit.LastName = parameters.LastName;
				if (!string.IsNullOrEmpty(parameters.Address))
					userToEdit.LastName = parameters.Address;
				if (!string.IsNullOrEmpty(parameters.MailAddress))
					userToEdit.LastName = parameters.MailAddress;
				if (parameters.PhoneNumber != null)
					userToEdit.PhoneNumber = parameters.PhoneNumber;
				userToEdit.IsEmailVerificationAllowed = parameters.IsEmailVerificationAllowed;
				userToEdit.IsSmsVerificationAllowed = parameters.IsSmsVerificationAllowed;

				await _dbContext.SaveChangesAsync();
				return userToEdit;
			}
			catch(Exception)
			{
				return null;
			}
		}
	}
}