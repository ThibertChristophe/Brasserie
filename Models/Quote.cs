using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models
{
    public class Quote{

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public decimal TotalPrice { get; set; }

        public long WholesalerId { get; set; }
	
		public Wholesaler? Wholesaler { get; set; }

        public ICollection<QuoteDetail>? Details { get; set; }

    }
}