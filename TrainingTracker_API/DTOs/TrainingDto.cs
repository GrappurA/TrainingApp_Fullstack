namespace TrainingApp.API.DTOs
{
	public class TrainingDto
	{
		public string Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public Guid UserId { get; set; } 
		

	}
}
