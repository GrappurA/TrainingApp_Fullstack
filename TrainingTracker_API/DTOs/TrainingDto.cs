namespace TrainingApp.API.DTOs
{
	public class TrainingDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Calories { get; set; } = 0;
		public DateOnly DateTime { get; set; } = DateOnly.MinValue;
		public int Duration { get; set; }
		public Guid UserId { get; set; }


	}
}
