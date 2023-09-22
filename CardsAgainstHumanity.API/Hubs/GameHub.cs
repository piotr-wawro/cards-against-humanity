using CardsAgainstHumanity.API.DataTransferObjects.GameDto;
using CardsAgainstHumanity.API.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CardsAgainstHumanity.API.Hubs;

public class GameHub : Hub {
    private readonly IGameService _gameService;
    private static readonly ConcurrentDictionary<string, List<string>> _gameConn = new();
    private static readonly ConcurrentDictionary<string, string?> _connGame = new();

    private async Task RemoveConnFromGame(string connectionId) {
        var gameId = _connGame[connectionId];
        if(gameId == null) return;

        _gameConn[gameId].Remove(connectionId);
        _connGame[connectionId] = null;
        _gameService.GetGame(gameId).RemoveUser(connectionId);
        await Groups.RemoveFromGroupAsync(connectionId, gameId);

        if(_gameConn[gameId].Count == 0) {
            _gameConn.TryRemove(gameId, out _);
            _gameService.DeleteGame(gameId);
        }
    }

    private async Task AddConnToGame(string gameId, string connectionId, string userName, bool isPlayer) {
        _gameConn[gameId].Add(connectionId);
        _connGame[connectionId] = gameId;
        await Groups.AddToGroupAsync(connectionId, gameId);
        if(isPlayer) _gameService.GetGame(gameId).AddPlayer(connectionId, userName);
        else _gameService.GetGame(gameId).AddSpectator(connectionId, userName);
    }

    public GameHub(IGameService gameService) {
        _gameService = gameService;
    }

    public override async Task OnConnectedAsync() {
        _connGame[Context.ConnectionId] = null;
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception) {
        await RemoveConnFromGame(Context.ConnectionId);
        _connGame.TryRemove(Context.ConnectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task CreateGame(string gameName, string? pass, int rounds, string userName) {
        var gameId = Guid.NewGuid().ToString();
        _gameConn[gameId] = new();
        _gameService.CreateGame(gameId, gameName, Context.ConnectionId, pass, rounds);
        await RemoveConnFromGame(Context.ConnectionId);
        await AddConnToGame(gameId, Context.ConnectionId, userName, true);
    }

    public async Task UpdateGame(GameUpdateDto update) {
        var connectionId = Context.ConnectionId;
        var groupId = _connGame[connectionId];
        if(groupId == null) return;

        var game = _gameService.GetGame(groupId);
        var ownerId = game.OwnerId;
        if(ownerId != connectionId) return;

        await game.Update(update);
    }

    public async Task JoinGame(string groupId, string? pass, string userName, bool isPlayer) {
        var game = _gameService.GetGame(groupId);
        if(game.Password != pass) return;
        await RemoveConnFromGame(Context.ConnectionId);
        await AddConnToGame(Context.ConnectionId, groupId, userName, isPlayer);
    }

    public async Task LeaveGame() {
        await RemoveConnFromGame(Context.ConnectionId);
    }

    public async Task SendGameMessage(string message) {
        var groupId = _connGame[Context.ConnectionId];
        if(groupId == null) return;
        await Clients.Group(groupId).SendAsync("ReceiveMessage", message);
    }
}
