
using Brasserie.DTOs.Beer;
using Brasserie.DTOs.Request;
using Brasserie.Models;

namespace Brasserie.Services
{
    public interface IBeerService{
        Task<List<BeerWithBrewerDTO>> GetAll();
        Task<BeerWithBrewerDTO> GetById(long id);
        Task<Beer> Create(CreateBeerRequest beer);
        Task<bool> Delete(BeerWithBrewerDTO beerDTO);
    }
}