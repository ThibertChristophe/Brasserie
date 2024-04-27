using Brasserie.DTOs;
using Brasserie.DTOs.Beer;
using Brasserie.DTOs.Request;
using Brasserie.Models;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Controllers
{
	[ApiController]
	[Route("api/beer")]
	public class BeerController : ControllerBase
	{
		private readonly BeerService _beerService;

		public BeerController(BeerService beerService)
		{
			this._beerService = beerService;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<List<BeerWithBrewerDTO>>> GetAllBeer()
		{
			List<BeerWithBrewerDTO> beers = await _beerService.GetAll();
			return Ok(beers);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<BeerWithBrewerDTO>> GetBeerById([FromRoute] long id)
		{
			BeerWithBrewerDTO beer = await _beerService.GetById(id);
			return Ok(beer);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<SimpleBeerDTO>> AddBeer(CreateBeerRequest beerDTO)
		{
			Beer result  = await _beerService.Create(beerDTO);
			return Created($"/api/beer/{result.Id}", result);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> DeleteBeer([FromRoute] int id)
		{
			BeerWithBrewerDTO beer = await _beerService.GetById(id);
			await _beerService.Delete(beer);
			return Ok();
		}


	}
}
