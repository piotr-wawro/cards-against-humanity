using CardsAgainstHumanity.API.DataTransferObjects.GameDto;
using CardsAgainstHumanity.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CardsAgainstHumanity.API.Services;

public class GameService : IGameService {
    private readonly IHubContext<GameHub> _hubContext;
    private readonly IDeckService _deckService;
    private static readonly ConcurrentDictionary<string, Game> _games = new();

    public GameService(IHubContext<GameHub> hubContext, IDeckService deckService) {
        _deckService = deckService;
        _hubContext = hubContext;
    }

    public Game GetGame(string gameId) {
        return _games[gameId];
    }

    public IEnumerable<GameDto> GetPage(GamePageQueryDto par) {
        return _games.Values.Skip(par.PageNumber * par.PageSize).Take(par.PageSize).Select(x =>
            new GameDto {
                Id = x.Id,
                Decks = x.Decks.Values.ToArray(),
                Players = x.Players.Values.Select(x => x.Name).ToArray(),
                Spectators = x.Spectators.Values.Select(x => x.Name).ToArray(),
            }
        );
    }

    public void CreateGame(string gameId, string name, string ownerId, string? pass, int rounds) {
        var game = new Game(_hubContext.Clients, _deckService, gameId, name, ownerId, pass, 10, rounds);
        _games[gameId] = game;
    }

    public void DeleteGame(string gameId) {
        var game = GetGame(gameId);
        if(game == null) return;
        _games.TryRemove(gameId, out Game? _);
    }
}
