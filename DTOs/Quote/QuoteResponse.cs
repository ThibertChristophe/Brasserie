using Brasserie.Models;

namespace Brasserie.DTOs
{
    public class QuoteResponseDTO{
      
       public double TotalPrice { get; set; }

       public double WholesalerName { get; set; }

       public List<SimpleBeerDTO> beerDTOs { get; set; } = [];

    }
}