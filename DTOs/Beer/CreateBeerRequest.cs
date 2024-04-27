using Brasserie.Models;

namespace Brasserie.DTOs.Request{
    public class CreateBeerRequest{
		public required string Name {  get; set; }
		public required double Price { get; set; }
		public required double AlcoholLevel { get; set; }
		public required long BrewerId { get; set; }
    }
}