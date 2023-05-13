using System.ComponentModel.DataAnnotations;

namespace Replacer.Api.Logic.Parameters
{
	public class CreateUserParameters
	{
		[Required] public string Login { get; set; } = string.Empty;
		[Required] public string Password { get; set; } = string.Empty;
		[Required] public string FirstName { get; set; } = string.Empty;
		[Required] public string LastName { get; set; } = string.Empty;
		public string MailAddress { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public bool IsEmailNotificationsAllowed { get; set; }
	}
}