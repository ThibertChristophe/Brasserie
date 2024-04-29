using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class StockService : IStockService
	{
		private readonly AppDbContext _context;

		public StockService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<StockDTO> GetStockByWholesalerAndBeer(long beerId, long wholesalerId)
		{
			Stock? stock = await _context.Stocks
				.Where(stock => stock.BeerId == beerId && stock.WholesalerId == wholesalerId)
				.FirstOrDefaultAsync();

			if (stock == null) throw new StockNotFoundException("No stock for this beer and/or wholesaler");

			StockDTO stockDTO = new ()
			{
				Id = stock.Id,
				BeerId = beerId,
				WholesalerId = wholesalerId,
				QuantityInStock = stock.QuantityInStock,
				UnitPrice = stock.UnitPrice,
			};
			return stockDTO;		
		}

		public async Task<StockDTO> Update(long idStock, StockDTO stock){
			if(idStock != stock.Id) throw new BadParameterException();
			if(stock.QuantityInStock < 0) throw new BadParameterException("Stock can't be negative");
			Stock? stockFound = await _context.Stocks.FindAsync(idStock);
			if(stockFound == null) throw new StockNotFoundException();

			stockFound.QuantityInStock = stock.QuantityInStock;
			stockFound.WholesalerId = stock.WholesalerId;
			stockFound.BeerId = stock.BeerId;
			stockFound.UnitPrice = stock.UnitPrice;
			await _context.SaveChangesAsync();

			return stock;
		}

		// Create stock
	}
}
