namespace Replacer.Api.Logic.Parameters
{
	public class UpdateUserParameters
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string MailAddress { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public bool IsEmailNotificationsAllowed { get; set; }
	}
}