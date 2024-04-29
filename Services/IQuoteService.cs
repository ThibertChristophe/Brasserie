using Brasserie.DTOs;
using Brasserie.Models;

namespace Brasserie.Services
{
    public interface IQuoteService
    {
        Task<QuoteDTO> GetById(long id);
        Task<Quote> CreateQuote(CreateQuoteRequest quoteDto) ;
        bool HaveDuplicate(List<CreateQuoteDetailRequest> quoteDetails);
        double ApplyDiscount(double totalPrice, int numItems);
    }
}