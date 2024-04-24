using Brasserie.DTOs;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Controllers
{
	[ApiController]
	[Route("api/sale")]
	public class SaleController : ControllerBase
	{

		private readonly StockService _stockService;
		private readonly SaleService _saleService;

		public SaleController(SaleService saleService, StockService stockService)
		{
			_saleService = saleService;
			_stockService = stockService;
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<SaleDTO>> AddSale(SaleDTO saleDTO)
		{
			// Vérifier si la biere existe en stock pour ce grossiste
			StockDTO? stock = await _stockService.GetStockByWholesalerAndBeer(saleDTO.BeerId, saleDTO.WholesalerId);

			if (stock == null) return BadRequest("The specified Beer does not exist for this wholesaler.");
			if (stock.QuantityInStock == 0) return BadRequest("Not more stock (stock = 0).");
			if (stock.QuantityInStock < saleDTO.Quantity) return BadRequest("Not enough stock.");


			//await _saleService.AddSale(saleDTO);
			await _stockService.DecreaseStockQuantityById(stock.Id, saleDTO.Quantity);

			return Ok();
		}
	}
}
