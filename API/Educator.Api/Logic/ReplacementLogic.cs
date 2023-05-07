using Educator.Api.Database;
using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Educator.Api.Logic
{
	public class ReplacementLogic : IReplacementLogic
	{
		private readonly EducatorDbContext _dbContext;
		public ReplacementLogic(EducatorDbContext educatorDbContext)
		{
			_dbContext = educatorDbContext;
		}

		public async Task<ReplacementDto?> CreateReplacement(CreateReplacementParameters parameters)
		{
			try
			{
				ReplacementStatus status = await _dbContext.ReplacementStatuses.Where(status => status.Name == "Available").SingleAsync();
				Enrollment? enrollment = await  _dbContext.Enrollments.FindAsync(parameters.EnrollmentId);
				if (enrollment == null)
					return null;

				Replacement newReplacement = new()
				{
					Date = parameters.Date,
					OldStudentId = enrollment.StudentId,
					EnrollmentId = parameters.EnrollmentId,
					StatusId = status.Id
				};
				await _dbContext.Replacements.AddAsync(newReplacement);
				await _dbContext.SaveChangesAsync();
				var replacementAdded = await (from replacement in _dbContext.Replacements
										  join enrollment1 in _dbContext.Enrollments on replacement.EnrollmentId equals enrollment1.Id
										  join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
										  join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
										  join oldStudent in _dbContext.Users on replacement.OldStudentId equals oldStudent.Id
										  join oldStudentRole in _dbContext.Roles on oldStudent.RoleId equals oldStudentRole.Id
										  join newStudent in _dbContext.Users on replacement.OldStudentId equals newStudent.Id
										  join newStudentRole in _dbContext.Roles on newStudent.RoleId equals newStudentRole.Id
										  join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
										  join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
										  join status1 in _dbContext.ReplacementStatuses on replacement.StatusId equals status1.Id
										  where replacement.Id == newReplacement.Id
										  select new ReplacementDto()
										  {
											  Id = replacement.Id,
											  Date = replacement.Date,
											  OldStudent = new()
											  {
												  Id = oldStudent.Id,
												  Login = oldStudent.Login,
												  FirstName = oldStudent.FirstName,
												  LastName = oldStudent.LastName,
												  MailAddress = oldStudent.MailAddress,
												  PhoneNumber = oldStudent.PhoneNumber,
												  Address = oldStudent.Address,
												  RoleName = oldStudentRole.Name,
												  IsEmailVerificationAllowed = oldStudent.IsEmailVerificationAllowed,
												  IsSmsVerificationAllowed = oldStudent.IsSmsVerificationAllowed
											  },
											  StatusId = replacement.StatusId,
											  StatusName = status1.Name,
											  SubjectName = subject.Name,
											  MaxLessonsQty = subjectDetail.MaxLessonsQty,
											  TeacherId = teacher.Id,
											  BuildingName = building.Name,
											  BuildingAddress = building.Address,
											  IsReplacementPossible = subjectDetail.IsReplacementPossible,
											  TeacherFirstName = teacher.FirstName,
											  TeacherLastName = teacher.LastName,
										  }).SingleOrDefaultAsync();
				return replacementAdded;
			}
			catch(Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<ReplacementDto>?> GetAvailableReplacements()
		{
			try
			{
				var replacements = await (from replacement in _dbContext.Replacements
										  join enrollment in _dbContext.Enrollments on replacement.EnrollmentId equals enrollment.Id
										  join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
										  join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
										  join oldStudent in _dbContext.Users on replacement.OldStudentId equals oldStudent.Id
										  join oldStudentRole in _dbContext.Roles on oldStudent.RoleId equals oldStudentRole.Id
										  join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
										  join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
										  join status in _dbContext.ReplacementStatuses on replacement.StatusId equals status.Id
										  where replacement.NewStudentId == null && status.Name == "Available"
										  select new ReplacementDto()
										  {
											  Id = replacement.Id,
											  Date = replacement.Date,
											  OldStudent = new()
											  {
												  Id = oldStudent.Id,
												  Login = oldStudent.Login,
												  FirstName = oldStudent.FirstName,
												  LastName = oldStudent.LastName,
												  MailAddress = oldStudent.MailAddress,
												  PhoneNumber = oldStudent.PhoneNumber,
												  Address = oldStudent.Address,
												  RoleName = oldStudentRole.Name,
												  IsEmailVerificationAllowed = oldStudent.IsEmailVerificationAllowed,
												  IsSmsVerificationAllowed = oldStudent.IsSmsVerificationAllowed
											  },
											  NewStudent = null,
											  StatusId = replacement.StatusId,
											  StatusName = status.Name,
											  SubjectName = subject.Name,
											  MaxLessonsQty = subjectDetail.MaxLessonsQty,
											  TeacherId = teacher.Id,
											  BuildingName = building.Name,
											  BuildingAddress = building.Address,
											  IsReplacementPossible = subjectDetail.IsReplacementPossible,
											  TeacherFirstName = teacher.FirstName,
											  TeacherLastName = teacher.LastName,
										  }).ToListAsync();
				return replacements;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<ReplacementDto>?> GetMyReplacements(int userId)
		{
			try
			{
				var replacements = await (from replacement in _dbContext.Replacements
										  join enrollment in _dbContext.Enrollments on replacement.EnrollmentId equals enrollment.Id
										  join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
										  join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
										  join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
										  join oldStudent in _dbContext.Users on replacement.OldStudentId equals oldStudent.Id
										  join oldStudentRole in _dbContext.Roles on oldStudent.RoleId equals oldStudentRole.Id
										  join newStudent in _dbContext.Users on replacement.OldStudentId equals newStudent.Id
										  join newStudentRole in _dbContext.Roles on newStudent.RoleId equals newStudentRole.Id
										  join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
										  join status in _dbContext.ReplacementStatuses on replacement.StatusId equals status.Id
										  where replacement.OldStudentId == userId || replacement.NewStudentId == userId
										  select new ReplacementDto()
										  {
											  Id = replacement.Id,
											  Date = replacement.Date,
											  OldStudent = new()
											  {
												  Id = oldStudent.Id,
												  Login = oldStudent.Login,
												  FirstName = oldStudent.FirstName,
												  LastName = oldStudent.LastName,
												  MailAddress = oldStudent.MailAddress,
												  PhoneNumber = oldStudent.PhoneNumber,
												  Address = oldStudent.Address,
												  RoleName = oldStudentRole.Name,
												  IsEmailVerificationAllowed = oldStudent.IsEmailVerificationAllowed,
												  IsSmsVerificationAllowed = oldStudent.IsSmsVerificationAllowed
											  },
											  NewStudent = new()
											  {
												  Id = newStudent.Id,
												  Login = newStudent.Login,
												  FirstName = newStudent.FirstName,
												  LastName = newStudent.LastName,
												  MailAddress = newStudent.MailAddress,
												  PhoneNumber = newStudent.PhoneNumber,
												  Address = newStudent.Address,
												  RoleName = newStudentRole.Name,
												  IsEmailVerificationAllowed = newStudent.IsEmailVerificationAllowed,
												  IsSmsVerificationAllowed = newStudent.IsSmsVerificationAllowed
											  },
											  StatusId = replacement.StatusId,
											  StatusName = status.Name,
											  SubjectName = subject.Name,
											  MaxLessonsQty = subjectDetail.MaxLessonsQty,
											  TeacherId = teacher.Id,
											  BuildingName = building.Name,
											  BuildingAddress = building.Address,
											  IsReplacementPossible = subjectDetail.IsReplacementPossible,
											  TeacherFirstName = teacher.FirstName,
											  TeacherLastName = teacher.LastName,
										  }).ToListAsync();
				return replacements;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<ReplacementDto?> GetReplacement(int id)
		{
			try
			{
				var replacementWithId = await (from replacement in _dbContext.Replacements
											   join enrollment in _dbContext.Enrollments on replacement.EnrollmentId equals enrollment.Id
											   join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
											   join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
											   join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
											   join oldStudent in _dbContext.Users on replacement.OldStudentId equals oldStudent.Id
											   join oldStudentRole in _dbContext.Roles on oldStudent.RoleId equals oldStudentRole.Id
											   join newStudent in _dbContext.Users on replacement.OldStudentId equals newStudent.Id
											   join newStudentRole in _dbContext.Roles on newStudent.RoleId equals newStudentRole.Id
											   join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
											   join status in _dbContext.ReplacementStatuses on replacement.StatusId equals status.Id
											   where replacement.Id == id
											   select new ReplacementDto()
											   {
												   Id = replacement.Id,
												   Date = replacement.Date,
												   OldStudent = new()
												   {
													   Id = oldStudent.Id,
													   Login = oldStudent.Login,
													   FirstName = oldStudent.FirstName,
													   LastName = oldStudent.LastName,
													   MailAddress = oldStudent.MailAddress,
													   PhoneNumber = oldStudent.PhoneNumber,
													   Address = oldStudent.Address,
													   RoleName = oldStudentRole.Name,
													   IsEmailVerificationAllowed = oldStudent.IsEmailVerificationAllowed,
													   IsSmsVerificationAllowed = oldStudent.IsSmsVerificationAllowed
												   },
												   NewStudent = new()
												   {
													   Id = newStudent.Id,
													   Login = newStudent.Login,
													   FirstName = newStudent.FirstName,
													   LastName = newStudent.LastName,
													   MailAddress = newStudent.MailAddress,
													   PhoneNumber = newStudent.PhoneNumber,
													   Address = newStudent.Address,
													   RoleName = newStudentRole.Name,
													   IsEmailVerificationAllowed = newStudent.IsEmailVerificationAllowed,
													   IsSmsVerificationAllowed = newStudent.IsSmsVerificationAllowed
												   },
												   StatusId = replacement.StatusId,
												   StatusName = status.Name,
												   SubjectName = subject.Name,
												   MaxLessonsQty = subjectDetail.MaxLessonsQty,
												   TeacherId = teacher.Id,
												   BuildingName = building.Name,
												   BuildingAddress = building.Address,
												   IsReplacementPossible = subjectDetail.IsReplacementPossible,
												   TeacherFirstName = teacher.FirstName,
												   TeacherLastName = teacher.LastName,
											   }).SingleOrDefaultAsync();
				return replacementWithId;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<ReplacementDto?> UpdateReplacement(UpdateReplacementParameters parameters)
		{
			//TODO
			Replacement? replacement = await _dbContext.Replacements.FindAsync(parameters.ReplacementId);
			var message = new MailMessage()
			{
				From = new MailAddress("empatyzacja@gmail.com", "EDUCATOR APP"),
				Subject = "Poznaj nasze nowości",
				Body = ""

			};
			message.To.Add(new MailAddress(""));
			byte[] reader = File.ReadAllBytes("");
			MemoryStream stream = new MemoryStream(reader);
			AlternateView av = AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html);
			LinkedResource headerImage = new (stream, MediaTypeNames.Image.Jpeg);
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

		public UserDto? GetUserById(int? id)
		{
			if (id == null)
				return null;
			var userWithId = (from user in _dbContext.Users
							  join role in _dbContext.Roles on user.RoleId equals role.Id
							  where user.IsActive == true && user.Id == id
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
							  }).SingleOrDefault();
			return userWithId;
		}
	}
}