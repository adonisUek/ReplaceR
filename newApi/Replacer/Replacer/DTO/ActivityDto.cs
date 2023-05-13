namespace Replacer.DTO
{
	public class ActivityDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime Date { get; set; }
		public UserDto? Creator { get; set; }
		public UserDto? NewUser { get; set; }
		public int StatusId { get; set; }
		public string StatusName { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
	}
}