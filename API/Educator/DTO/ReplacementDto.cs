namespace Educator.DTO
{
	public class ReplacementDto
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public UserDto? OldStudent { get; set; }
		public UserDto? NewStudent { get; set; }
		public int StatusId { get; set; }
		public string StatusName { get; set; } = string.Empty;
		public string SubjectName { get; set; } = string.Empty;
		public int TeacherId { get; set; }
		public string TeacherFirstName { get; set; } = string.Empty;
		public string TeacherLastName { get; set; } = string.Empty;
		public string BuildingName { get; set; } = string.Empty;
		public string BuildingAddress { get; set; } = string.Empty;
		public int? MaxLessonsQty { get; set; }
		public bool IsReplacementPossible { get; set; }
	}
}