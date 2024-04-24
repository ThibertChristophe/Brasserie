using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class BrewerService
	{
       private readonly AppDbContext _context;

		public BrewerService(AppDbContext context) { 
			_context = context;
		}

        public async Task<BrewerDTO?> GetById(long id){
            Brewer? brewer = await _context.Brewers
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(p=>p.Id == id);
            if (brewer == null) return null;
            BrewerDTO brewerDTO = new ()
			{
				Id = brewer.Id,
				Name = brewer.Name,
                Beers =  brewer.Beers.Select(beer => new BeerDTO
                {
                    Id = beer.Id,
                    Name = beer.Name,
                    Price = beer.Price,
                    AlcoholLevel = beer.AlcoholLevel,
                    BrewerId = beer.BrewerId,
                
                }).ToList()
			};
            return brewerDTO;
        }
        public async Task<List<BrewerDTO>> GetAllBrewerWithBeers(){
            List<Brewer> brewersWithBeers = await _context.Brewers
                                .Include(b=>b.Beers)
                                .ToListAsync();

            List<BrewerDTO> brewerDTOs = brewersWithBeers.Select(brewer => new BrewerDTO
            {
                Id = brewer.Id,
                Name = brewer.Name,
                Beers =  brewer.Beers.Select(beer => new BeerDTO
                {
                    Id = beer.Id,
                    Name = beer.Name,
                    Price = beer.Price,
                    AlcoholLevel = beer.AlcoholLevel,
                    BrewerId = beer.BrewerId,
                   
                }).ToList()
            }).ToList();

            return brewerDTOs;
        }

        // Liste des bières pour un Brasseur
        public async Task<List<BeerDTO>> GetBeersFromBrewer(long brewerId){
            List<BeerDTO> beerDTOs = [];
            List<Beer> beers = await _context.Beers
                                            .Where(b => b.BrewerId == brewerId)
                                            .ToListAsync();
            beers.ForEach(beer => {
                beerDTOs.Add(new BeerDTO{
                    Id = beer.Id,
                    Name = beer.Name,
                    AlcoholLevel = beer.AlcoholLevel,
                    Price = beer.Price,

                });
            });
            return beerDTOs;
        }

       
    }
}