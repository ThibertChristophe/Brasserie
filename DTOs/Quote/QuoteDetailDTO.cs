namespace Brasserie.DTOs
{
    public class QuoteDetailDTO{
        public long Id { get; set; }
        public long BeerId { get; set; }
        
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}