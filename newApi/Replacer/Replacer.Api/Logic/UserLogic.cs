using Replacer.Api.Database;
using Replacer.Api.Logic.Interfaces;
using Replacer.Api.Logic.Parameters;
using Replacer.DbModels;
using Replacer.DTO;
using Microsoft.EntityFrameworkCore;
namespace Replacer.Api.Logic
{
	public class UserLogic : IUserLogic
	{
		private readonly ReplacerDbContext _dbContext;
		public UserLogic(ReplacerDbContext replacerDbContext)
		{
			_dbContext = replacerDbContext;
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
					IsEmailNotificationsAllowed = parameters.IsEmailNotificationsAllowed,
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
										   where  user.Login == login
										   select new UserAuthDto()
										   {
											   Login = user.Login,
											   Password = user.PasswordHash,
											   Id = user.Id,
										   }).SingleOrDefaultAsync();
				return userWithLogin;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<UserDto?> GetUser(int id)
		{
			try
			{
				var user1 = await (from user in _dbContext.Users
								   where user.Id == id
								   select new UserDto()
								   {
									   Id = user.Id,
									   Login = user.Login,
									   FirstName = user.FirstName,
									   LastName = user.LastName,
									   PhoneNumber = user.PhoneNumber,
									   MailAddress = user.MailAddress,
									   Address = user.Address,
									   IsEmailNotificationsAllowed = user.IsEmailNotificationsAllowed,
								   }).FirstOrDefaultAsync();
				return user1;
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
				if (!string.IsNullOrEmpty(parameters.FirstName))
					userToEdit.FirstName = parameters.FirstName;
				if (!string.IsNullOrEmpty(parameters.LastName))
					userToEdit.LastName = parameters.LastName;
				if (!string.IsNullOrEmpty(parameters.Address))
					userToEdit.Address = parameters.Address;
				if (!string.IsNullOrEmpty(parameters.MailAddress))
					userToEdit.MailAddress = parameters.MailAddress;
				if (parameters.PhoneNumber != null)
					userToEdit.PhoneNumber = parameters.PhoneNumber;
				userToEdit.IsEmailNotificationsAllowed = parameters.IsEmailNotificationsAllowed;

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