namespace Replacer.DbModels
{
	public class Activity
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime Date { get; set; }
		public int CreatorId { get; set; }
		public int? NewUserId { get; set; }
		public int StatusId { get; set; }
		public string City { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
	}
}
