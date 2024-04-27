namespace Brasserie.DTOs.Sale
{
    public class SaleDTO{
        public long Id { get; set; }
        public long WholesalerId { get; set; }
		
		public long BeerId { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }
    }
}