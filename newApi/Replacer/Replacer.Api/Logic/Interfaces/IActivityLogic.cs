using Replacer.Api.Logic.Parameters;
using Replacer.DbModels;
using Replacer.DTO;

namespace Replacer.Api.Logic.Interfaces
{
    public interface IActivityLogic
    {
		Task<IEnumerable<ActivityDto>?> GetActivities(int userId);
		Task<IEnumerable<ActivityDto>?> GetMyActivities(int userId);
		Task<ActivityDto?> GetActivity(int id);
		Task<IEnumerable<Activity>?> CreateActivity(CreateActivityParameters parameters);
		Task<Activity?> UpdateActivity(int id, UpdateActivityParameters parameters);
		Task<bool> DeleteActivity(int id);
    }
}