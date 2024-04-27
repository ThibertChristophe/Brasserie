using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.DTOs.Response;
using Brasserie.Exceptions;
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

        public async Task<BrewerWithBeerDTO> GetById(long id){
            Brewer? brewer = await _context.Brewers
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(p=>p.Id == id);

            if (brewer == null) throw new BrewerNotFoundException();

            BrewerWithBeerDTO brewerDTO = new ()
			{
				Id = brewer.Id,
				Name = brewer.Name,
                Beers =  brewer.Beers.Select(beer => new SimpleBeerDTO
                {
                    Id = beer.Id,
                    Name = beer.Name,
                    Price = beer.Price,
                    AlcoholLevel = beer.AlcoholLevel
                
                }).ToList()
			};
            return brewerDTO;
        }
        public async Task<List<BrewerWithBeerDTO>> GetAllBrewerWithBeers(){
            List<Brewer> brewersWithBeers = await _context.Brewers
                                .Include(b=>b.Beers)
                                .ToListAsync();

            List<BrewerWithBeerDTO> brewerDTOs = brewersWithBeers.Select(brewer => new BrewerWithBeerDTO
            {
                Id = brewer.Id,
                Name = brewer.Name,
                Beers =  brewer.Beers.Select(beer => new SimpleBeerDTO
                {
                    Id = beer.Id,
                    Name = beer.Name,
                    Price = beer.Price,
                    AlcoholLevel = beer.AlcoholLevel
                   
                   
                }).ToList()
            }).ToList();

            return brewerDTOs;
        }

        // Liste des bières pour un Brasseur
        public async Task<List<SimpleBeerDTO>> GetBeersFromBrewer(long brewerId){
            Brewer? brewer = await _context.Brewers
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(p=>p.Id == brewerId);
            if (brewer == null) throw new BrewerNotFoundException();
            
            List<SimpleBeerDTO> beerDTOs = [];
            List<Beer> beers = await _context.Beers
                                            .Where(b => b.BrewerId == brewerId)
                                            .ToListAsync();
            beers.ForEach(beer => {
                beerDTOs.Add(new SimpleBeerDTO{
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