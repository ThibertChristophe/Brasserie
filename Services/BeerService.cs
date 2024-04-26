﻿using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class BeerService
	{
		private readonly AppDbContext _context;

		public BeerService(AppDbContext context) { 
			this._context = context;
		}
		
		public async Task<List<BeerDTO>> GetAll()
		{
			List<BeerDTO> result = [];

			List<Beer> beers = await _context.Beers.ToListAsync();
			beers.ForEach(beer =>
			{
				result.Add(new BeerDTO
				{
					Id = beer.Id,
					Name = beer.Name,
					AlcoholLevel = beer.AlcoholLevel,
					Price = beer.Price,
					BrewerId = beer.BrewerId,
				});
			});
			return result;
		}

		public async Task<BeerDTO> GetById(long id)
		{
			Beer? beer = await _context.Beers.FindAsync(id);
			if (beer == null) throw new BeerNotFoundException();

			BeerDTO result = new ()
			{
				Id = beer.Id,
				Name = beer.Name,
				AlcoholLevel = beer.AlcoholLevel,
				Price = beer.Price
			};
			return result;
		}

		public async Task<Beer> Create(BeerDTO beer)
		{
			if (beer == null) throw new BadParameterException();
			Beer beerResult = new()
			{
				Id = beer.Id,
				Name = beer.Name,
				AlcoholLevel = beer.AlcoholLevel,
				Price = beer.Price,
				BrewerId= beer.BrewerId,
			};

			_context.Beers.Add(beerResult);
			await _context.SaveChangesAsync();
			return beerResult;
		}

		public async Task<bool> Delete(BeerDTO beerDTO)
		{
			Beer? beer = await _context.Beers.FindAsync(beerDTO.Id);
			if(beer == null) return false;

			_context.Beers.Remove(beer);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
