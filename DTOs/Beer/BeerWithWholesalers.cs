using Brasserie.DTOs.Wholesaler;

namespace Brasserie.DTOs.Beer
{
    public class BeerWithWholesalers
    {
		public long Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public double AlcoholLevel { get; set; }
        public List<SimpleWholesaler> Wholesalers { get; set; }
    }
}