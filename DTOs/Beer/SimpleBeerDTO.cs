using Brasserie.Models;

namespace Brasserie.DTOs
{
	public class SimpleBeerDTO
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public double AlcoholLevel { get; set; }
	}
}
