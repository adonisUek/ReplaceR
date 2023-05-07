using Educator.Api.Logic.Parameters;
using Educator.DbModels;
using Educator.DTO;

namespace Educator.Api.Logic.Interfaces
{
	public interface IReplacementLogic
	{
		Task<IEnumerable<ReplacementDto>?> GetMyReplacements(int userId);
		Task<ReplacementDto?> GetReplacement(int id);
		Task<IEnumerable<ReplacementDto>?> GetAvailableReplacements();
		Task<ReplacementDto?> UpdateReplacement(UpdateReplacementParameters parameters);
		Task<ReplacementDto?> CreateReplacement(CreateReplacementParameters parameters);
	}
}