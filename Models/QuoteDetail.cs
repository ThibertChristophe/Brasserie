using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models{

    public class QuoteDetail{

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        public long BeerId { get; set; }
		public Beer? Beer { get; set; }

        public int Quantity { get; set; }
	
		public long QuoteId { get; set; }
		public virtual Quote? Quote { get; set; }

        public double Price { get; set; }
	}
}