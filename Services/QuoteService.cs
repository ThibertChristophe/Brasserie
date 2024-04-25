using Brasserie.Data;
using Brasserie.DTOs;
using Brasserie.Exceptions;
using Brasserie.Models;

namespace Brasserie.Services
{
    public class QuoteService{

        private readonly AppDbContext _context;
        public QuoteService(AppDbContext context){
            _context = context;
        }
        
        // Ajouter un Quote et ses Details
        // Verif si stock suffisant
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