using Brasserie.Models;

namespace Brasserie.DTOs.Beer
{
	public class BeerWithBrewerDTO
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public double AlcoholLevel { get; set; }
		public required SimpleBrewerDTO Brewer { get; set; }
	}
}
