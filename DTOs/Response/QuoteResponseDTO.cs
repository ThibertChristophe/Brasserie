using Brasserie.Models;

namespace Brasserie.DTOs
{
    public class QuoteResponseDTO{
      
       double TotalPrice { get; set; }

       double WholesalerName { get; set; }

       List<BeerDTO> beerDTOs { get; set; } = [];

    }
}