namespace TrainingTracker.Models
{
	public class Training
	{
		public int Id { get; set; }
		public int TrainingId { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public double Calories { get; set; }
		public DateOnly DateTime { get; set; }
		public int Duration { get; set; }
	}
}
