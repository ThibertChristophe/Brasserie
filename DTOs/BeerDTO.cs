using Brasserie.Models;

namespace Brasserie.DTOs
{
	public class BeerDTO
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public double AlcoholLevel { get; set; }
		public long BrewerId { get; set; }
	}
}
