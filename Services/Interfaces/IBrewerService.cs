using Brasserie.DTOs;
using Brasserie.DTOs.Brewer;


namespace Brasserie.Services
{
    public interface IBrewerService
    {
        Task<BrewerWithBeerDTO> GetById(long id);
        Task<List<BrewerWithBeerDTO>> GetAllBrewerWithBeers();
        Task<List<SimpleBeerDTO>> GetBeersFromBrewer(long brewerId);
        Task DeleteBeerForBrewer(long idBrewer, long idBeer);
        Task<List<BrewerWithBeerAndWholesalers>> GetAllWithBeerWithWholesaler();
    }
}