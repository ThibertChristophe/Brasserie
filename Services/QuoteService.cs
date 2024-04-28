using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.DTOs.Beer;
using Brasserie.Exceptions;
using Brasserie.Models;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Services
{
    public class QuoteService {

        private readonly AppDbContext _context;
        private readonly StockService _stockService;
        private readonly BeerService _beerService;
      

        public QuoteService(AppDbContext context, StockService stockService, BeerService beerService) {
            _context = context;
            _stockService = stockService;
            _beerService = beerService;
        }

        public async Task<QuoteDTO> GetById(long id)
        {
			Quote? quote = await _context.Quotes
			    .Include(q => q.Details)
				.FirstOrDefaultAsync(q => q.Id == id);

            if (quote == null) throw new QuoteNotFoundException();

            QuoteDTO quoteDto = new() {
                Id = quote.Id,
				WholesalerId = quote.WholesalerId,
                TotalPrice = quote.TotalPrice
			};

            List<QuoteDetailDTO> detailsDtos = quote.Details.Select(detail => new QuoteDetailDTO
            {
				Id = detail.Id,
				BeerId = detail.BeerId,
				Quantity = detail.Quantity,
                Price = detail.Price,

			}).ToList();
            quoteDto.Details = detailsDtos;
			return quoteDto;

		}

        // Creer la Response avec le recap
        public async Task<Quote> CreateQuote(CreateQuoteRequest quoteDto) {
            if (quoteDto == null) throw new BadParameterException("Null not valid");
            // Details non vide
            if (quoteDto.Details == null || quoteDto.Details.Count <= 0) throw new BadParameterException("Null or empty List");
            // Grossiste existant
            Wholesaler? wholesaler = await _context.Wholesalers.FindAsync(quoteDto.WholesalerId);
            if (wholesaler == null) throw new WholesalerFoundException("Wholesaler not found");
            // Pas de doublon dans Details
            if (HaveDuplicate(quoteDto.Details)) throw new BadParameterException("Duplicate details/beer not allowed");

            Quote quoteModel = new()
			{
				WholesalerId = quoteDto.WholesalerId,
                TotalPrice = 0
                
			};
            List<QuoteDetail> details = [];
            double totalPrice = 0;

            foreach (var detail in quoteDto.Details){
                // TODO : enhance with a function able to check stock for a list of beer
			    // Verif si stock suffisant
                StockDTO stockDto = await _stockService.GetStockByWholesalerAndBeer(detail.BeerId, quoteDto.WholesalerId);
                if (stockDto.QuantityInStock < detail.Quantity) throw new BadParameterException($"Not enough stock for the beer id : {detail.BeerId}");
                // Prix de la biere du stock du wholesaler
                totalPrice += stockDto.UnitPrice * detail.Quantity;
                details.Add(new QuoteDetail{
                    BeerId = detail.BeerId,
                    Quantity = detail.Quantity,
                    QuoteId = quoteModel.Id,
                    Price = stockDto.UnitPrice * detail.Quantity,
                });
            }
			
			quoteModel.Details = details;

            // Discount
            totalPrice = ApplyDiscount(totalPrice,quoteDto.Details.Count);
          
            quoteModel.TotalPrice = totalPrice;
			await _context.Quotes.AddAsync(quoteModel); 
			await _context.SaveChangesAsync();

			return quoteModel;
        }
    

        private bool HaveDuplicate(List<CreateQuoteDetailRequest> quoteDetails){
            HashSet<long> set = new HashSet<long>();
            List<long> duplicates = new List<long>();
            foreach (CreateQuoteDetailRequest item in quoteDetails)
            {
                if (!set.Add(item.BeerId))
                {
                    duplicates.Add(item.BeerId);
                }
            }
            if(duplicates.Count > 0) return true;
            return false;
        }

        private double ApplyDiscount(double totalPrice, int numItems)
        {
            double discount = 0.0;
            if (numItems > 20) {
                discount = 0.2;  // 20% discount
            } else if (numItems > 10) {
                discount = 0.1;  // 10% discount
            }
            return totalPrice * (1 - discount);
        }

    }
}