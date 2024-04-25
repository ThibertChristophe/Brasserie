using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Exceptions;
using Brasserie.Models;

namespace Brasserie.Services
{
    public class QuoteService{

        private readonly AppDbContext _context;
        private readonly StockService _stockService;
        public QuoteService(AppDbContext context, StockService stockService){
            _context = context;
            _stockService = stockService;
        }

        // Ajouter un Quote et ses Details
        // Discount -10% si > 10 boissons
        // Discount -20% si > 20 boissons
        public async Task<Quote> CreateQuote(QuoteDTO quote){
            if(quote==null) throw new ArgumentNullException("");
            // Details non vide
            if(quote.Details==null || quote.Details.Count <= 0) throw new Exception("Empty order not admitted");
            // Grossiste existant
            Wholesaler? wholesaler = await _context.Wholesalers.FindAsync(quote.WholesalerId);
            if(wholesaler==null) throw new WholesalerFoundException();
            // Pas de doublon dans Details
            if(HaveDuplicate(quote.Details)) throw new Exception("Can´t have duplicate beer in the order");
            
             // Verif si stock suffisant
            quote.Details.ForEach(async (detail)=>{
                // TODO : enhance with a function able to check stock for a list of beer
                StockDTO stockDto = await _stockService.GetStockByWholesalerAndBeer(detail.BeerId,quote.WholesalerId);
                if(stockDto.QuantityInStock < detail.Quantity){
                    throw new Exception($"Not enough stock for the beer id : {detail.BeerId}");
                }
            });


            return new Quote();
        }

        public bool HaveDuplicate(List<QuoteDetailDTO> quoteDetails){
            HashSet<long> set = new HashSet<long>();
            List<long> duplicates = new List<long>();
            foreach (QuoteDetailDTO item in quoteDetails)
            {
                if (!set.Add(item.Id))
                {
                    duplicates.Add(item.Id);
                }
            }
            if(duplicates.Count > 0) return true;
            return false;
        }
    }
}