using Brasserie.DTOs.Beer;

namespace Brasserie.DTOs.Brewer
{
    public class BrewerWithBeerAndWholesalers
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public List<BeerWithWholesalersDTO> Beers { get; set; } = [];
    }
}