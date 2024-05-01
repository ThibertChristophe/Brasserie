using Brasserie.DTOs.Wholesaler;

namespace Brasserie.DTOs.Beer
{
    public class BeerWithWholesalersDTO
    {
		public long Id { get; set; }
		public required string Name { get; set; }
		public double Price { get; set; }
		public double AlcoholLevel { get; set; }
        public List<SimpleWholesalerDTO> Wholesalers { get; set; } = [];
    }
}