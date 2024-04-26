using Brasserie.Models;

namespace Brasserie.DTOs
{
	public class BrewerDTO
	{
		public long Id { get; set; }
		public required string Name { get; set; }

		public required List<BeerDTO> Beers { get; set; }
	}
}