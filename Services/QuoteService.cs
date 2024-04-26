using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
    public class QuoteService {

        private readonly AppDbContext _context;
        private readonly StockService _stockService;

        public QuoteService(AppDbContext context, StockService stockService) {
            _context = context;
            _stockService = stockService;
        }

        public async Task<QuoteDTO> GetById(long id)
        {
			Quote? quote = await _context.Quotes
			    .Include(b => b.Details)
				.FirstOrDefaultAsync(p => p.Id == id);

            if (quote == null) throw new QuoteNotFoundException();

            QuoteDTO quoteDto = new() {
                Id = quote.Id,
				WholesalerId = quote.WholesalerId,
			};

            List<QuoteDetailDTO> detailsDtos = quote.Details.Select(detail => new QuoteDetailDTO
            {
				Id = detail.Id,
				BeerId = detail.BeerId,
				Quantity = detail.Quantity,
			}).ToList();
            quoteDto.Details = detailsDtos;
			return quoteDto;

		}

        // Ajouter un Quote et ses Details
        // Modifier le stock
        // Discount -10% si > 10 boissons
        // Discount -20% si > 20 boissons
        public async Task<Quote> CreateQuote(QuoteDTO quoteDto) {
            if (quoteDto == null) throw new BadParameterException("Null not valid");
            // Details non vide
            if (quoteDto.Details == null || quoteDto.Details.Count <= 0) throw new BadParameterException("Null or empty List");
            // Grossiste existant
            Wholesaler? wholesaler = await _context.Wholesalers.FindAsync(quoteDto.WholesalerId);
            if (wholesaler == null) throw new WholesalerFoundException("Wholesaler not found");
            // Pas de doublon dans Details
            if (HaveDuplicate(quoteDto.Details)) throw new BadParameterException("Duplicate details not allowed");

			// Verif si stock suffisant
			quoteDto.Details.ForEach(async(detail) => {
                // TODO : enhance with a function able to check stock for a list of beer
                StockDTO stockDto = await _stockService.GetStockByWholesalerAndBeer(detail.BeerId, quoteDto.WholesalerId);
                if (stockDto.QuantityInStock < detail.Quantity)
                {
					throw new BadParameterException($"Not enough stock for the beer id : {detail.BeerId}");
				}
            });

			Quote quoteModel = new()
			{
				WholesalerId = quoteDto.WholesalerId,
                TotalPrice = 20
                
			};
			List<QuoteDetail> details = quoteDto.Details.Select(dto => new QuoteDetail
			{
				BeerId = dto.BeerId,
				Quantity = dto.Quantity,
                QuoteId = quoteModel.Id
			}).ToList();

			quoteModel.Details = details;

			await _context.Quotes.AddAsync(quoteModel); 
			await _context.SaveChangesAsync();
			return quoteModel;
        }
    

        public bool HaveDuplicate(List<QuoteDetailDTO> quoteDetails){
            HashSet<long> set = new HashSet<long>();
            List<long> duplicates = new List<long>();
            foreach (QuoteDetailDTO item in quoteDetails)
            {
                if (!set.Add(item.BeerId))
                {
                    duplicates.Add(item.BeerId);
                }
            }
            if(duplicates.Count > 0) return true;
            return false;
        }
    }
}