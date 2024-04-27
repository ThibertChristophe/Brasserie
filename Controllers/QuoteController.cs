using Brasserie.DTOs;
using Brasserie.Models;
using Brasserie.Services;
using Microsoft.AspNetCore.Mvc;

namespace Brasserie.Controllers
{
	[ApiController]
	[Route("api/quote")]
	public class QuoteController : ControllerBase
	{
		private readonly QuoteService _quoteService;

		public QuoteController(QuoteService quoteService)
		{
			_quoteService = quoteService;
		}


		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<QuoteDTO>> GetQuoteById([FromRoute] long id)
		{
			QuoteDTO quoteDto = await _quoteService.GetById(id);
			return Ok(quoteDto);
		}

		[HttpPost]
		[ProducesResponseType(201)]
		public async Task<ActionResult<QuoteDTO>> AddQuote([FromBody] CreateQuoteRequest quoteDto)
		{
			Quote result = await _quoteService.CreateQuote(quoteDto);
			return Ok();
			//return Created($"/api/quote/{result.Id}",result);
		}
	}
}
