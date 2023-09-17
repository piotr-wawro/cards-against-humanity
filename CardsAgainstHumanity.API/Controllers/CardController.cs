using AutoMapper;
using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardsAgainstHumanity.API.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class CardController : ControllerBase {
    private readonly ICardService _cardService;

    public CardController(ICardService cardService) {
        _cardService = cardService;
    }

    [HttpGet("{id}")]
    public ActionResult<CardDto> GetCard([FromRoute] int id) {
        var card = _cardService.GetCard(id);
        if(card == null) return NotFound();
        return Ok(card);
    }

    [HttpGet]
    public ActionResult<IEnumerable<CardDto>> GetPage([FromQuery] CardPageQueryParams par ) {
        var cards = _cardService.GetPage(par);
        return Ok(cards);
    }

    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult<CardDto> CreateCard([FromBody] CreateCardDto par) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var card = _cardService.CreateCard(userId, par);
        return CreatedAtAction(nameof(CreateCard), new { id = card.Id }, card);
    }

    [HttpPatch]
    [Authorize(Policy = "User")]
    public ActionResult<CardDto> UpdateCard([FromBody] UpdateCardDto par) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var card = _cardService.UpdateCard(userId, par);
        if(card == null) return NotFound();
        return Ok(card);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult DeleteCard([FromRoute] int id) {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        _cardService.DeleteCard(userId, id);
        return Ok();
    }
}
