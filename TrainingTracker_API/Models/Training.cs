namespace TrainingTracker.Models
{
	public class Training
	{
		public int Id { get; set; }
		public int TrainingId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Calories { get; set; }
		public DateTime DateTime { get; set; }
		public int Duration { get; set; }
	}
}
