namespace LostFoundApi.Models
{
	public class LostItem
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Place { get; set; } = "";
		public bool IsReturned { get; set; }
	}
}