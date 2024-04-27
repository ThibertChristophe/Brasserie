using Brasserie.Models;

namespace Brasserie.DTOs
{
    public class QuoteDTO{
        public long Id { get; set; }

        public long WholesalerId { get; set; }
        public double TotalPrice { get; set; }

        public List<QuoteDetailDTO>? Details { get; set; }

    }
}