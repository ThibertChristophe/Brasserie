using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models
{
	public class Sale
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		public long WholesalerId { get; set; }
		public virtual Wholesaler? Wholesaler { get; set; }

		public long BeerId { get; set; }
		public virtual Beer? Beer { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }
	}
}
