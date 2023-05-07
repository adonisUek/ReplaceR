namespace Educator.DTO
{
	public class SubjectDto
	{
		public int SubjectDetailId { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; } = string.Empty;
		public string SubjectAdditionalInfo { get; set; } = string.Empty;
		public int TeacherId { get; set; }
		public string TeacherFirstName { get; set; } = string.Empty;
		public string TeacherLastName { get; set; } = string.Empty;
		public int? MaxLessonsQty { get; set; }
		public bool IsReplacementPossible { get; set; }
		public int BuildingId { get; set; }
		public string BuildingName { get; set; } = string.Empty;
		public string BuildingAddress { get; set; } = string.Empty;
	}
}
