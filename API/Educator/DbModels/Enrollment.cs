namespace Educator.DbModels
{
	public class Enrollment
	{
        public int Id { get; set; }
		public int SubjectDetailId { get; set; }
		public int StudentId { get; set; }
	}
}