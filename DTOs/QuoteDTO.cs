using Brasserie.Models;

namespace Brasserie.DTOs
{
    public class QuoteDTO{
        public long QuoteId { get; set; }

        public long WholesalerId { get; set; }

        public List<QuoteDetailDTO>? Details { get; set; }

    }
}