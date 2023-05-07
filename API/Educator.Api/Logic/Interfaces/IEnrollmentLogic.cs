using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;

namespace Educator.Api.Logic.Interfaces
{
    public interface IEnrollmentLogic
    {
		Task<IEnumerable<EnrollmentDto>?> GetEnrollments(int userId, bool withReplacementOnly);
		Task<EnrollmentDto?> GetEnrollment(int id);
		Task<IEnumerable<Enrollment>?> CreateEnrollments(CreateEnrollmentParameters[] parameters);
		Task<bool> DeleteEnrollment(int id);
    }
}