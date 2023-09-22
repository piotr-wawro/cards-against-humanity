using CardsAgainstHumanity.API.DataTransferObjects.GameDto;
using CardsAgainstHumanity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardsAgainstHumanity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : Controller {
    private readonly IGameService _gameService;

    public GameController(IGameService gameService) {
        _gameService = gameService;
    }

    [HttpGet("{id}")]
    public ActionResult<GameDto> GetGame([FromRoute] string id) {
        return Ok(_gameService.GetGame(id));
    }

    [HttpGet]
    public ActionResult<IEnumerable<GameDto>> GetPage([FromQuery] GamePageQueryDto par) {
        return Ok(_gameService.GetPage(par));
    }
}
