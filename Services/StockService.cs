using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
	public class StockService
	{
		private readonly AppDbContext _context;

		public StockService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<StockDTO?> GetStockByWholesalerAndBeer(long beerId, long wholesalerId)
		{
			Stock? stock = await _context.Stocks
				.Where(stock => stock.BeerId == beerId && stock.WholesalerId == wholesalerId)
				.FirstOrDefaultAsync();

			if(stock != null)
			{
				StockDTO stockDTO = new StockDTO
				{
					Id = stock.Id,
					BeerId = beerId,
					WholesalerId = wholesalerId,
					QuantityInStock = stock.QuantityInStock,
				};
				return stockDTO;
			}
			return null;
		}

		public async Task<bool> DecreaseStockQuantityById(long stockId, int quantityToRemove)
		{
			// Recup la ligne de stock
			Stock? stock = await _context.Stocks.FindAsync(stockId);
			// Check si on a bien cet article et si stock suffisant
			if (stock != null && stock.QuantityInStock >= quantityToRemove)
			{
				stock.QuantityInStock = stock.QuantityInStock - quantityToRemove;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> IncreaseStockQuantityById(long stockId, int quantityToAdd)
		{
			// Recup la ligne de stock
			Stock? stock = await _context.Stocks.FindAsync(stockId);
			// Check si on a bien cet article 
			if (stock != null)
			{
				stock.QuantityInStock = stock.QuantityInStock + quantityToAdd;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
