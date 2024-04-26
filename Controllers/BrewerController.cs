
using Brasserie.DTOs;
using Brasserie.Models;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Controllers
{
    [ApiController]
    [Route("api/brewer")]
    public class BrewerController : ControllerBase{
        private readonly BrewerService _brewerService;

        public BrewerController(BrewerService brewerService){
            _brewerService = brewerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrewerDTO>>> GetAll(){
            List<BrewerDTO> brewers = await _brewerService.GetAllBrewerWithBeers();
            return Ok(brewers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BrewerDTO>> GetById([FromRoute] long id){
            BrewerDTO brewer = await _brewerService.GetById(id);
            return Ok(brewer);
        }

        [HttpGet("{id}/beers")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<BeerDTO>>> GetBeers([FromRoute] long id){
            List<BeerDTO> beers = await _brewerService.GetBeersFromBrewer(id);
            return Ok(beers);
        }
    }
}