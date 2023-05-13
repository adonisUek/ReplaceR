using Educator.Api.Database;
using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Http;

namespace Educator.Api.Logic
{
	public class ActivityLogic : IActivityLogic
	{
		private readonly EducatorDbContext _dbContext;
		public ActivityLogic(EducatorDbContext educatorDbContext)
		{
			_dbContext = educatorDbContext;
		}
		public async Task<bool> DeleteActivity(int id)
		{
			try
			{
				Activity? activityToDelete = await _dbContext.Activities.Where(a => a.Id == id).SingleOrDefaultAsync();
				if (activityToDelete == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);
				_dbContext.Activities.Remove(activityToDelete);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<ActivityDto?> GetActivity(int id)
		{
			try
			{
				var activityWithId = await (from activity in _dbContext.Activities
											join status in _dbContext.ActivityStatuses on activity.StatusId equals status.Id
											select new ActivityDto()
											{
												//todo
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
										where activity.CreatorId != userId
										select new ActivityDto()
										{
											//todo
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
									where activity.CreatorId == userId || activity.NewUserId == userId
									select new ActivityDto()
									{
										//todo
									}).ToListAsync();

			return activities;
		}

		public async Task<IEnumerable<Activity>?> CreateActivity(CreateActivityParameters parameters)
		{
			try
			{
				Activity activity = new()
				{
					//todo
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