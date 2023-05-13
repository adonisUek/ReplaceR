using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;

namespace Educator.Api.Logic.Interfaces
{
    public interface IActivityLogic
    {
		Task<IEnumerable<ActivityDto>?> GetActivities(int userId);
		Task<IEnumerable<ActivityDto>?> GetMyActivities(int userId);
		Task<ActivityDto?> GetActivity(int id);
		Task<IEnumerable<Activity>?> CreateActivity(CreateActivityParameters parameters);
		Task<Activity?> UpdateActivity(UpdateActivityParameters parameters);
		Task<bool> DeleteActivity(int id);
    }
}