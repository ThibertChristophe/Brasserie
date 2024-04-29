using Brasserie.DTOs;
using Brasserie.DTOs.Response;

namespace Brasserie.Services
{
    public interface IBrewerService
    {
        public  Task<BrewerWithBeerDTO> GetById(long id);
        public Task<List<BrewerWithBeerDTO>> GetAllBrewerWithBeers();
        public Task<List<SimpleBeerDTO>> GetBeersFromBrewer(long brewerId);
        public Task DeleteBeerForBrewer(long idBrewer, long idBeer);
    }
}