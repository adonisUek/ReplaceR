namespace Educator.DbModels
{
	public class Replacement
	{
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
		public DateTime Date { get; set; }
		public int OldStudentId { get; set; }
		public int? NewStudentId { get; set; }
		public int StatusId { get; set; }
	}
}