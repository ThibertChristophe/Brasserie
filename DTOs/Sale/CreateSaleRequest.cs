using Brasserie.Models;

namespace Brasserie.DTOs
{
	public class CreateSaleRequest
	{
		
		public long WholesalerId { get; set; }
		
		public long BeerId { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

	}
}
