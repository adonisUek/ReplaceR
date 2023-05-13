namespace Replacer.Api.Logic.Parameters
{
	public class CreateActivityParameters
	{
		public string Name { get; set; } = string.Empty;
		public DateTime Date { get; set; }
		public int CreatorId { get; set; }
		public string City { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
	}
}