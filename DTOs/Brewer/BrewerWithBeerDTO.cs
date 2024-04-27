namespace Brasserie.DTOs.Response
{
    public class BrewerWithBeerDTO{
        public long Id { get; set; }
        public required string Name { get; set; }

        public List<SimpleBeerDTO> Beers { get; set; } = [];
    }
}