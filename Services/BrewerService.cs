using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.DTOs.Beer;
using Brasserie.DTOs.Brewer;
using Brasserie.DTOs.Wholesaler;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class BrewerService : IBrewerService
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

        public async Task DeleteBeerForBrewer(long idBrewer, long idBeer){
            Beer? beer = await _context.Beers.FindAsync(idBeer);
            if (beer == null) throw new BeerNotFoundException();
            if(beer.BrewerId != idBrewer) throw new BadParameterException();
            
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }

       	// Allez chercher les stocks des bieres et leurs grossistes
		public async Task<List<BrewerWithBeerAndWholesalers>> GetAllWithBeerWithWholesaler()
		{
			List<BrewerWithBeerAndWholesalers> result = [];

			List<Brewer> brewers = await _context.Brewers
					.Include(b => b.Beers)
					.OrderBy(b=>b.Id)
					.ToListAsync();

			foreach (var brewer in brewers)
			{
				BrewerWithBeerAndWholesalers brewerDto = new() {
					Id = brewer.Id,
					Name = brewer.Name
				};

                foreach( var beer in brewer.Beers){

                    List<Stock> stocks = await _context.Stocks.Include(s=>s.Wholesaler).Where(s=>s.BeerId == beer.Id).ToListAsync();
                    List<SimpleWholesaler> whosalersDto = [];

                    foreach (var stock in stocks){
                        SimpleWholesaler wholesaler = new(){
                            Name = stock.Wholesaler.Name
                        };
                        whosalersDto.Add(wholesaler);
                    }

                    brewerDto.Beers.Add(new BeerWithWholesalers
                    {
                        Id = beer.Id,
                        Name = beer.Name,
                        AlcoholLevel = beer.AlcoholLevel,
                        Price = beer.Price,
                        Wholesalers = whosalersDto
                    });
                }
				result.Add(brewerDto);
			}
			return result;
		}
    }
}