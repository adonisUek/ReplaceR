using System.ComponentModel.DataAnnotations;

namespace Replacer.Api.Logic.Parameters
{
	public class UpdateActivityParameters
	{
		[Required] public int OldStatusId { get; set; }
		[Required] public int NewStatusId { get; set;}
		public int? OldUserId { get; set; }
		public int? NewUserId { get; set; }
	}
}