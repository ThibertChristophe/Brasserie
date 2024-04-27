using Brasserie.Models;

namespace Brasserie.DTOs
{
	public class StockDTO
	{
		public long Id { get; set; }

		public long WholesalerId { get; set; }
		
		public long BeerId { get; set; }

		public int QuantityInStock { get; set; }

		public double UnitPrice { get; set; }
	}
}
