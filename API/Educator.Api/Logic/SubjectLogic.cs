using Educator.Api.Database;
using Educator.Api.Logic.Interfaces;
using Educator.DTO;
using Microsoft.EntityFrameworkCore;

namespace Educator.Api.Logic
{
	public class SubjectLogic : ISubjectLogic
	{
		private readonly EducatorDbContext _dbContext;
		public SubjectLogic(EducatorDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<SubjectDto>?> GetSubjects()
		{
			try
			{
				var subjects = await (from subjectDetail in _dbContext.SubjectDetails
									  join subject in _dbContext.Subjects on subjectDetail.SubjectId equals subject.Id
									  join teacher in _dbContext.Users on subjectDetail.TeacherId equals teacher.Id
									  join building in _dbContext.Buildings on subjectDetail.BuildingId equals building.Id
									  select new SubjectDto()
									  {
										  SubjectId = subject.Id,
										  SubjectName = subject.Name,
										  SubjectDetailId = subjectDetail.Id,
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
				return subjects;
			}
			catch(Exception)
			{
				return null;
			}
		}
	}
}
