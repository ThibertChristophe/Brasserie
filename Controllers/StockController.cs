using Brasserie.DTOs;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Controllers
{
	[ApiController]
	[Route("api/stock")]
	public class StockController : ControllerBase
	{
        private readonly StockService _stockService;
        public StockController(StockService service){
            _stockService = service;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(long id, StockDTO newStock){
            StockDTO stockDTO = await _stockService.Update(id, newStock);
            return Ok(stockDTO);
        }
    }
}