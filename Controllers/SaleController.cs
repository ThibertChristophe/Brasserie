using Brasserie.DTOs;
using Brasserie.DTOs.Sale;
using Brasserie.Models;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brasserie.Controllers
{
	[ApiController]
	[Route("api/sale")]
	public class SaleController : ControllerBase
	{

		private readonly IStockService _stockService;
		private readonly ISaleService _saleService;

		public SaleController(ISaleService saleService, IStockService stockService)
		{
			_saleService = saleService;
			_stockService = stockService;
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<SaleDTO>> AddSale(CreateSaleRequest saleRequest)
		{
			// Vérifier si la biere existe en stock pour ce grossiste
			StockDTO stock = await _stockService.GetStockByWholesalerAndBeer(saleRequest.BeerId, saleRequest.WholesalerId);

			if (stock == null) return BadRequest("The specified Beer does not exist for this wholesaler.");
			if (stock.QuantityInStock == 0) return BadRequest("Not more stock (stock = 0).");
			if (stock.QuantityInStock < saleRequest.Quantity) return BadRequest("Not enough stock.");

			Sale result = await _saleService.AddSale(saleRequest);
			
			return Created($"api/sale/{result.Id}", result);
		}
	}
}
