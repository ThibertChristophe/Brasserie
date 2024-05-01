using Brasserie.Data;

using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;
using Brasserie.DTOs.Beer;
using Brasserie.DTOs;
using Brasserie.DTOs.Request;
using Brasserie.DTOs.Wholesaler;
using Brasserie.DTOs.Brewer;

namespace Brasserie.Services
{
	public class BeerService : IBeerService
	{
		private readonly AppDbContext _context;

		public BeerService(AppDbContext context) { 
			this._context = context;
		}
		
		public async Task<List<BeerWithBrewerDTO>> GetAll()
		{
			List<BeerWithBrewerDTO> result = [];

			List<Beer> beers = await _context.Beers
					.Include(beer => beer.Brewer)
					.OrderBy(p=>p.BrewerId)
					.ToListAsync();

			beers.ForEach(beer =>
			{
				SimpleBrewerDTO brewerDto = new() {
					Id = beer.Brewer.Id,
					Name = beer.Brewer.Name
				};

				result.Add(new BeerWithBrewerDTO
				{
					Id = beer.Id,
					Name = beer.Name,
					AlcoholLevel = beer.AlcoholLevel,
					Price = beer.Price,
					Brewer = brewerDto
				});
			});
			return result;
		}

		public async Task<BeerWithBrewerDTO> GetById(long id)
		{
			Beer? beer = await _context.Beers
				.Include(beer => beer.Brewer)
				.FirstOrDefaultAsync(p=>p.Id == id);
			if (beer == null) throw new BeerNotFoundException();

			SimpleBrewerDTO brewerDto = new() {
				Id = beer.Brewer.Id,
				Name = beer.Brewer.Name
			};

			BeerWithBrewerDTO result = new ()
			{
				Id = beer.Id,
				Name = beer.Name,
				AlcoholLevel = beer.AlcoholLevel,
				Price = beer.Price,
				Brewer = brewerDto
			};
			return result;
		}

		public async Task<Beer> Create(CreateBeerRequest beer)
		{
			if (beer == null) throw new BadParameterException();

			Beer beerResult = new()
			{
				Name = beer.Name,
				AlcoholLevel = beer.AlcoholLevel,
				Price = beer.Price,
				BrewerId = beer.BrewerId
			};

			_context.Beers.Add(beerResult);
			await _context.SaveChangesAsync();
			return beerResult;
		}

		public async Task<bool> Delete(BeerWithBrewerDTO beerDTO)
		{
			Beer? beer = await _context.Beers.FindAsync(beerDTO.Id);
			if(beer == null) return false;

			_context.Beers.Remove(beer);
			await _context.SaveChangesAsync();
			return true;
		}

	
	}
}
