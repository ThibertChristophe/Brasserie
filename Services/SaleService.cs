using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.DTOs.Sale;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class SaleService : ISaleService
	{
		private readonly AppDbContext _context;

		public SaleService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Sale> AddSale(CreateSaleRequest saleRequest)
		{
			ArgumentNullException.ThrowIfNull(saleRequest);
			// ajoute une sale dans la table Sale
			Beer? beer = await _context.Beers.FindAsync(saleRequest.BeerId);
			if(beer == null) throw new BeerNotFoundException();
			Wholesaler? wholesaler = await _context.Wholesalers.FindAsync(saleRequest.WholesalerId);
			if (wholesaler == null) throw new WholesalerFoundException();

			// Verif si stock ?
			Stock stock = await _context.Stocks.Where(s=>s.BeerId == beer.Id).Where(s=>s.WholesalerId == wholesaler.Id).FirstAsync();

			Sale saleToAdd = new (){
				BeerId = beer.Id,
				WholesalerId = wholesaler.Id,
				// Si prix venant du stock ou prix fixé par la vente ?
				//Price = stock.UnitPrice * saleRequest.Quantity,
				Price = saleRequest.Price,
				Quantity = saleRequest.Quantity
			};
			
			await _context.Sales.AddAsync(saleToAdd);
			await _context.SaveChangesAsync();

			return saleToAdd;
		}
	}
}
