using Educator.Api.Database;
using Educator.Api.Logic.Interfaces;
using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;

namespace Educator.Api.Logic
{
	public class EnrollmentLogic : IEnrollmentLogic
	{
		private readonly EducatorDbContext _dbContext;
		public EnrollmentLogic(EducatorDbContext educatorDbContext)
		{
			_dbContext = educatorDbContext;
		}
		public async Task<IEnumerable<Enrollment>?> CreateEnrollments(CreateEnrollmentParameters[] parametersArray)
		{
			try
			{
				foreach (CreateEnrollmentParameters parameters in parametersArray)
				{
					Enrollment enrollment = new()
					{
						StudentId = parameters.StudentId,
						SubjectDetailId = parameters.SubjectDetailId
					};
					await _dbContext.Enrollments.AddAsync(enrollment);
				}
				await _dbContext.SaveChangesAsync();
				return await _dbContext.Enrollments.ToListAsync();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeleteEnrollment(int id)
		{
			try
			{
				Enrollment? enrollmentToDelete = await _dbContext.Enrollments.Where(enrollment => enrollment.Id == id).SingleOrDefaultAsync();
				if (enrollmentToDelete == null)
					throw new HttpResponseException(HttpStatusCode.NotFound);
				_dbContext.Enrollments.Remove(enrollmentToDelete);
				await _dbContext.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<EnrollmentDto?> GetEnrollment(int id)
		{
			try
			{
				var enrollmentWithId = await (from enrollment in _dbContext.Enrollments
											  join student in _dbContext.Users on enrollment.StudentId equals student.Id
											  join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
											  join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
											  join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
											  join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
											  where student.IsActive == true && enrollment.Id == id
											  select new EnrollmentDto()
											  {
												  Id = enrollment.Id,
												  StudentId = student.Id,
												  SubjectDetailId = subjectDetail.Id,
												  SubjectId = subject.Id,
												  SubjectName = subject.Name,
												  SubjectAdditionalInfo = subject.AdditionalInfo,
												  TeacherId = teacher.Id,
												  TeacherFirstName = teacher.FirstName,
												  TeacherLastName = teacher.LastName,
												  BuildingId = building.Id,
												  BuildingName = building.Name,
												  BuildingAddress = building.Address,
												  IsReplacementPossible = subjectDetail.IsReplacementPossible,
												  MaxLessonsQty = subjectDetail.MaxLessonsQty
											  }).SingleOrDefaultAsync();
				return enrollmentWithId;
			}
			catch(Exception)
			{
				return null;
			}
		}

		public async Task<IEnumerable<EnrollmentDto>?> GetEnrollments(int userId, bool withReplacementOnly)
		{
			try
			{
				var enrollments = await (from enrollment in _dbContext.Enrollments
										 join student in _dbContext.Users on enrollment.StudentId equals student.Id
										 join subjectDetail in _dbContext.SubjectDetails on enrollment.SubjectDetailId equals subjectDetail.Id
										 join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
										 join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
										 join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
										 where student.IsActive == true && student.Id == userId
										 select new EnrollmentDto()
										 {
											 Id = enrollment.Id,
											 StudentId = student.Id,
											 SubjectDetailId = subjectDetail.Id,
											 SubjectId = subject.Id,
											 SubjectName = subject.Name,
											 SubjectAdditionalInfo = subject.AdditionalInfo,
											 TeacherId = teacher.Id,
											 TeacherFirstName = teacher.FirstName,
											 TeacherLastName = teacher.LastName,
											 BuildingId = building.Id,
											 BuildingName = building.Name,
											 BuildingAddress = building.Address,
											 IsReplacementPossible = subjectDetail.IsReplacementPossible,
											 MaxLessonsQty = subjectDetail.MaxLessonsQty
										 }).ToListAsync();

				if (withReplacementOnly == true)
					enrollments = (List<EnrollmentDto>)enrollments.Where(e => e.IsReplacementPossible == true).ToList();

				return enrollments;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}