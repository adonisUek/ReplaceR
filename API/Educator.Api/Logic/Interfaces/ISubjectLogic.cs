using Educator.DTO;

namespace Educator.Api.Logic.Interfaces
{
    public interface ISubjectLogic
    {
		Task<IEnumerable<SubjectDto>?> GetSubjects();
	}
}