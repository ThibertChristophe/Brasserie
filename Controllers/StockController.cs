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

        
    }
}