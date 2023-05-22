using Replacer.Api.Database;
using Replacer.Api.Logic.Interfaces;
using Replacer.Api.Logic.Parameters;
using Replacer.DbModels;
using Replacer.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Replacer.Api.Logic
{
	public class ActivityLogic : IActivityLogic
	{
		private readonly ReplacerDbContext _dbContext;
		public ActivityLogic(ReplacerDbContext replacerDbContext)
		{
			_dbContext = replacerDbContext;
		}

		public async Task<ActivityDto?> GetActivity(int id)
		{
			try
			{
				var activityWithId = await (from activity in _dbContext.Activities
											join status in _dbContext.ActivityStatuses on activity.StatusId equals status.Id
											join creator in _dbContext.Users on activity.CreatorId equals creator.Id
											join newUsers in _dbContext.Users on activity.NewUserId equals newUsers.Id into newUserNullable
											from newUser in newUserNullable.DefaultIfEmpty()
											where activity.Id == id
											select new ActivityDto()
											{
												Id = activity.Id,
												Name = activity.Name,
												Date = activity.Date,
												Creator = new UserDto()
												{
													Id = creator.Id,
													Login = creator.Login,
													FirstName = creator.FirstName,
													LastName = creator.LastName,
													PhoneNumber = creator.PhoneNumber,
													MailAddress = creator.MailAddress,
													Address = creator.Address,
													IsEmailNotificationsAllowed = creator.IsEmailNotificationsAllowed,
												},
												NewUser = newUser == null ? null : new UserDto()
												{
													Id = newUser.Id,
													Login = newUser.Login,
													FirstName = newUser.FirstName,
													LastName = newUser.LastName,
													PhoneNumber = newUser.PhoneNumber,
													MailAddress = newUser.MailAddress,
													Address = newUser.Address,
													IsEmailNotificationsAllowed = newUser.IsEmailNotificationsAllowed,
												},
												StatusId = activity.StatusId,
												StatusName = status.Name,
												City = activity.City,
												Address = activity.Address
											}).SingleOrDefaultAsync();
				return activityWithId;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<ActivityDto>?> GetActivities(int userId)
		{
			try
			{
				var activities = await (from activity in _dbContext.Activities
										join status in _dbContext.ActivityStatuses on activity.StatusId equals status.Id
										join creator in _dbContext.Users on activity.CreatorId equals creator.Id
										join newUsers in _dbContext.Users on activity.NewUserId equals newUsers.Id into newUserNullable
										from newUser in newUserNullable.DefaultIfEmpty()
										where status.Name == "Available" && activity.CreatorId != userId
										select new ActivityDto()
										{
											Id = activity.Id,
											Name = activity.Name,
											Date = activity.Date,
											Creator = new UserDto()
											{
												Id = creator.Id,
												Login = creator.Login,
												FirstName = creator.FirstName,
												LastName = creator.LastName,
												PhoneNumber = creator.PhoneNumber,
												MailAddress = creator.MailAddress,
												Address = creator.Address,
												IsEmailNotificationsAllowed = creator.IsEmailNotificationsAllowed,
											},
											NewUser = newUser == null ? null : new UserDto()
											{
												Id = newUser.Id,
												Login = newUser.Login,
												FirstName = newUser.FirstName,
												LastName = newUser.LastName,
												PhoneNumber = newUser.PhoneNumber,
												MailAddress = newUser.MailAddress,
												Address = newUser.Address,
												IsEmailNotificationsAllowed = newUser.IsEmailNotificationsAllowed,
											},
											StatusId = activity.StatusId,
											StatusName = status.Name,
											City = activity.City,
											Address = activity.Address
										}).ToListAsync();

				return activities;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<ActivityDto>?> GetMyActivities(int userId)
		{
			var activities = await (from activity in _dbContext.Activities
									join status in _dbContext.ActivityStatuses on activity.StatusId equals status.Id
									join creator in _dbContext.Users on activity.CreatorId equals creator.Id
									join newUsers in _dbContext.Users on activity.NewUserId equals newUsers.Id into newUserNullable
									from newUser in newUserNullable.DefaultIfEmpty()
									where activity.CreatorId == userId || activity.NewUserId == userId
									select new ActivityDto()
									{
										Id = activity.Id,
										Name = activity.Name,
										Date = activity.Date,
										Creator = new UserDto()
										{
											Id = creator.Id,
											Login = creator.Login,
											FirstName = creator.FirstName,
											LastName = creator.LastName,
											PhoneNumber = creator.PhoneNumber,
											MailAddress = creator.MailAddress,
											Address = creator.Address,
											IsEmailNotificationsAllowed = creator.IsEmailNotificationsAllowed,
										},
										NewUser = newUser == null ? null : new UserDto()
										{
											Id = newUser.Id,
											Login = newUser.Login,
											FirstName = newUser.FirstName,
											LastName = newUser.LastName,
											PhoneNumber = newUser.PhoneNumber,
											MailAddress = newUser.MailAddress,
											Address = newUser.Address,
											IsEmailNotificationsAllowed = newUser.IsEmailNotificationsAllowed,
										},
										StatusId = activity.StatusId,
										StatusName = status.Name,
										City = activity.City,
										Address = activity.Address
									}).ToListAsync();
			return activities;
		}

		public async Task<IEnumerable<Activity>?> CreateActivity(CreateActivityParameters parameters)
		{
			try
			{
				Activity activity = new()
				{
					Name = parameters.Name,
					Date = parameters.Date,
					CreatorId = parameters.CreatorId,
					City = parameters.City,
					Address = parameters.Address,
					StatusId = 1
				};
				await _dbContext.Activities.AddAsync(activity);
				await _dbContext.SaveChangesAsync();
				return await _dbContext.Activities.ToListAsync();
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<bool> DeleteActivity(int id)
		{
			try
			{
				Activity? activityToDelete = await _dbContext.Activities.Where(a => a.Id == id).SingleOrDefaultAsync();
				if (activityToDelete == null)
					return false;
				_dbContext.Activities.Remove(activityToDelete);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<Activity?> UpdateActivity(int id, UpdateActivityParameters parameters)
		{
			//TODO
			Activity? activity = await _dbContext.Activities.FindAsync(id);
			User? newUser = await _dbContext.Users.FindAsync(parameters.NewUserId);
			User? oldUser = await _dbContext.Users.FindAsync(parameters.OldUserId);
			if (activity == null)
				throw new ApplicationException("Nie znaleziono aktywności o id" + id);
			if (oldUser == null)
				throw new ApplicationException("Nie znaleziono użytkownika (oldUserId) o id" + parameters.OldUserId);
			if (newUser == null)
				throw new ApplicationException("Nie znaleziono użytkownika (newUserId) o id" + parameters.NewUserId);

			MailMessage message = new()
			{
				From = new MailAddress("empatyzacja@gmail.com", "REPLACER APP"),
				Subject = "Zmiana statusu aktywności w aplikacji Replacer"
			};

			if (!string.IsNullOrEmpty(oldUser?.MailAddress))
				message.To.Add(new MailAddress(oldUser.MailAddress));
			if (!string.IsNullOrEmpty(newUser?.MailAddress))
				message.To.Add(new MailAddress(newUser.MailAddress));

			activity.StatusId = parameters.NewStatusId;

			//rezerwacja dostępnej aktywności
			if (parameters.OldStatusId == 1 && parameters.NewStatusId == 2)
			{
				activity.NewUserId = parameters.NewUserId;
				message.Body = $"Użytkownik {newUser?.FirstName} {newUser?.LastName} ({newUser?.Login}) zarezerwował aktywość: {activity?.Name}, " +
					$"którą utworzył użytkownik {oldUser?.FirstName} {oldUser?.LastName} ({oldUser?.Login})" +
					$"Aktywność odbędzie się {activity?.Date} w lokalizacji: {activity?.Address}, {activity?.City}";
			}

			//użytkownik który zarezerwował anuluje rezerwację
			if (parameters.OldStatusId == 2 && parameters.NewStatusId == 1)
			{
				message.Body = $"Użytkownik {newUser?.FirstName} {newUser?.LastName} ({newUser?.Login}) anulował rezerwację aktywości: {activity?.Name}, " +
					$"którą utworzył użytkownik {oldUser?.FirstName} {oldUser?.LastName} ({oldUser?.Login})" +
					$"Szczegóły aktywności: data: {activity?.Date.ToString()}, lokalizacja: {activity?.Address}, {activity?.City}";
			}

			//twórca odwołuje już zarezerwowaną aktywność
			if (parameters.OldStatusId == 2 && parameters.NewStatusId == 3)
			{
				message.Body = $"Użytkownik {oldUser?.FirstName} {oldUser?.LastName} ({oldUser?.Login}) odwołał aktywość: {activity?.Name}, " +
					$"Szczegóły aktywności: data: {activity?.Date.ToString()}, lokalizacja: {activity?.Address}, {activity?.City}";
			}

			//twórca odwołuje aktywność która jest dostępna
			if (parameters.OldStatusId == 1 && parameters.NewStatusId == 3)
			{
				throw new ApplicationException("Do odwoływania nie zarezerwowanych aktywności słuzy metoda delete activity, zaś przywracanie anulowanych aktywności nie jest obsługiwane");
			}

			await _dbContext.SaveChangesAsync();

			SmtpClient smtp = new()
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("empatyzacja@gmail.com", "Empatyzacja1!")
			};
			try
			{
				foreach (MailAddress address in message.To.ToArray())
					smtp.SendAsync(message, address);
			}
			catch (SmtpException ex)
			{
				throw new ApplicationException("Klient SMTP wywołał wyjątek. Sprawdź połączenie z internetem." + ex.Message);
			}

			throw new NotImplementedException();
		}
	}
}