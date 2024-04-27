namespace Brasserie.DTOs
{
    public class CreateQuoteRequest{
      
        public long WholesalerId { get; set; }
    
        public List<CreateQuoteDetailRequest>? Details { get; set; }

    }
}