using Brasserie.Data;
using Brasserie.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Services
{
	public class SaleService
	{
		private readonly AppDbContext _context;

		public SaleService(AppDbContext context)
		{
			_context = context;
		}

		public void AddSale([FromBody] SaleDTO saleDTO)
		{
			ArgumentNullException.ThrowIfNull(saleDTO);
			// ajoute une sale dans la table Sale
			// Modifie le stock ( le reduit)


			return;
		}
	}
}
