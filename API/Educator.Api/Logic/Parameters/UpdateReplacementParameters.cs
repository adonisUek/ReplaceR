using System.ComponentModel.DataAnnotations;

namespace Educator.Api.Logic.Parameters
{
	public class UpdateReplacementParameters
	{
		[Required] public int ReplacementId { get; set; }
		[Required] public int OldStatusId { get; set; }
		[Required] public int NewStatusId { get; set;}
		public int? NewUserId { get; set; }
	}
}