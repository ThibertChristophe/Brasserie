
using Brasserie.DTOs;
using Brasserie.DTOs.Brewer;
using Brasserie.Models;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Controllers
{
    [ApiController]
    [Route("api/brewer")]
    public class BrewerController : ControllerBase{
        private readonly IBrewerService _brewerService;

        public BrewerController(IBrewerService brewerService){
            _brewerService = brewerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrewerWithBeerDTO>>> GetAll(){
            List<BrewerWithBeerDTO> brewers = await _brewerService.GetAllBrewerWithBeers();
            return Ok(brewers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BrewerWithBeerDTO>> GetById([FromRoute] long id){
            BrewerWithBeerDTO brewer = await _brewerService.GetById(id);
            return Ok(brewer);
        }

        [HttpGet("{id}/beers")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<SimpleBeerDTO>>> GetBeers([FromRoute] long id){
            List<SimpleBeerDTO> beers = await _brewerService.GetBeersFromBrewer(id);
            return Ok(beers);
        }

        [HttpDelete("{id}/beer/{idBeer}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteBeerByIdForBrewer(long id, long idBeer){
            await _brewerService.DeleteBeerForBrewer(id, idBeer);
            return NoContent();
        }

        [HttpGet("/test")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<List<BrewerWithBeerDTO>>> ListBeer()
		{
			List<BrewerWithBeerAndWholesalers> beers = await _brewerService.GetAllWithBeerWithWholesaler();
			return Ok(beers);
		}
    }
}