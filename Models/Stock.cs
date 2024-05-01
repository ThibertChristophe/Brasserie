using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models
{
	public class Stock
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		public long WholesalerId { get; set; }
		[ForeignKey("WholesalerId")] // cascade de delete si on efface un wholesaler
		public virtual Wholesaler Wholesaler { get; set; }

		public long BeerId { get; set; }
		public virtual Beer Beer { get; set; }

		public int QuantityInStock { get; set; }

		public double UnitPrice { get; set; }
	}
}
