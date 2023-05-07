namespace Educator.DbModels
{
	public class SubjectDetail
	{
		public int Id { get; set; }
		public int SubjectId { get; set; }
		public int TeacherId { get; set; }
		public int BuildingId { get; set; }
		public int? MaxLessonsQty { get; set; }
		public bool IsReplacementPossible { get; set; }
	}
}