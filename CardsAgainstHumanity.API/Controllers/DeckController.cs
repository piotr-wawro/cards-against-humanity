using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.API.DataTransferObjects.DeckDto;
using CardsAgainstHumanity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardsAgainstHumanity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeckController : ControllerBase {
    private readonly IDeckService _deckService;

    public DeckController(IDeckService deckService) {
        _deckService = deckService;
    }

    [HttpGet("{id}")]
    public ActionResult<DeckDto> GetDeck([FromRoute] int id) {
        var deck = _deckService.GetDeck(id);
        if(deck == null) return NotFound();
        return Ok(deck);
    }

    [HttpGet]
    public ActionResult<IEnumerable<DeckDto>> GetPage([FromQuery] DeckPageQueryParams par ) {
        var decks = _deckService.GetPage(par);
        return Ok(decks);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public ActionResult<DeckDto> CreateDeck([FromBody] CreateDeckDto par) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
        var deck = _deckService.CreateDeck(userId, par);
        return CreatedAtAction(nameof(CreateDeck), new { id = deck.Id }, deck);
    }

    [HttpPatch]
    [Authorize(Roles = "User")]
    public ActionResult<DeckDto> UpdateDeck([FromBody] UpdateDeckDto par) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
        var deck = _deckService.UpdateDeck(userId, par);
        if(deck == null) return NotFound();
        return Ok(deck);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "User")]
    public ActionResult DeleteDeck([FromRoute] int id) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.Name).Value);
        _deckService.DeleteDeck(userId, id);
        return Ok();
    }
}
