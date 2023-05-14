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
											join newUsers in _dbContext.Users on activity.NewUserId equals newUsers.Id into newUserNullable from newUser in newUserNullable.DefaultIfEmpty()
											where activity.Id == id
											select new ActivityDto()
											{
												Id = activity.Id,
												Name=activity.Name,
												Date=activity.Date,
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

		public async Task<Activity?> UpdateActivity(UpdateActivityParameters parameters)
		{
			//TODO
			Activity? activity = await _dbContext.Activities.FindAsync(parameters.ActivityId);
			var message = new MailMessage()
			{
				From = new MailAddress("empatyzacja@gmail.com", "REPLACER APP"),
				Subject = "Poznaj nasze nowości",
				Body = ""
			};
			message.To.Add(new MailAddress(""));
			byte[] reader = File.ReadAllBytes("");
			MemoryStream stream = new MemoryStream(reader);
			AlternateView av = AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html);
			LinkedResource headerImage = new(stream, MediaTypeNames.Image.Jpeg);
			headerImage.ContentId = "newsletter";
			headerImage.ContentType = new ContentType("image/jpg");
			av.LinkedResources.Add(headerImage);
			message.AlternateViews.Add(av);
			ContentType mimeType = new ContentType("text/html");
			AlternateView alternate = AlternateView.CreateAlternateViewFromString(message.Body, mimeType);
			message.AlternateViews.Add(alternate);
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
				smtp.SendAsync(message, message.To.First());
			}
			catch (SmtpException ex)
			{
				throw new ApplicationException("Klient SMTP wywołał wyjątek. Sprawdź połączenie z internetem." + ex.Message);
			}

			throw new NotImplementedException();
		}
	}
}