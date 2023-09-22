using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.API.DataTransferObjects.GameDto;
using CardsAgainstHumanity.API.Services;
using CardsAgainstHumanity.DatabaseAccess.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CardsAgainstHumanity.API;

public enum GameState {
    Lobby,
    Question,
    Answer,
}

public class Player {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int Score { get; set; } = 0;
    public List<CardDto> Cards { get; set; } = new();
    public bool IsReady { get; set; } = false;
}

public class PlayedCard {
    public required string PlayerId { get; set; }
    public required string PlayerName { get; set; }
    public int CardId { get; set; }
}

public class Game {
    private readonly IHubClients _hubClients;
    private readonly IDeckService _deckService;
    private readonly Random rng = new();

    public string Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public string? Password { get; set; }
    public int MaxPlayers { get; set; }
    public int MaxRounds { get; set; }
    public GameState GameState { get; set; } = GameState.Lobby;
    public long NextState { get; set; } = 0;
    public int Round { get; set; } = 0;
    public ConcurrentDictionary<string, Player> Players { get; set; } = new();
    public ConcurrentDictionary<string, Player> Spectators { get; set; } = new();
    public ConcurrentStack<Player> CardCzar { get; set; } = new();
    public Dictionary<int, string> Decks { get; set; } = new();
    public Queue<CardDto> BlackCardsStack { get; set; } = new();
    public List<CardDto> BlackCards { get; set; } = new();
    public Queue<CardDto> WhiteCardsStack { get; set; } = new();
    public List<CardDto> WhiteCards { get; set; } = new();
    public ConcurrentStack<PlayedCard> PlayedCards { get; set; } = new();

    private void SendGameData(string userId) {
        _hubClients.User(userId).SendAsync("SetGameState", new {
            id = Id,
            name = Name,
            owner = OwnerId,
            maxPlayers = MaxPlayers,
            maxRounds = MaxRounds,
            gameState = GameState,
            nextState = NextState,
            round = Round,
            players = Players,
            spectators = Spectators,
            cardCzar = CardCzar,
            decks = Decks,
        });
    }

    private async Task SendCards(Player[] players) {
        foreach(var player in players) {
            var toDraw = 10 - player.Cards.Count;
            var cards = new List<CardDto>();
            for(int i = 0; i < toDraw; i++) {
                try {
                    cards.Add(WhiteCardsStack.Dequeue());
                }
                catch(InvalidOperationException) {
                    WhiteCardsStack = new Queue<CardDto>(WhiteCards);
                    WhiteCardsStack.OrderBy(a => rng.Next()).ToList();
                    cards.Add(WhiteCardsStack.Dequeue());
                }
            }
            await _hubClients.User(player.Id).SendAsync("AddCards", cards);
            player.Cards.AddRange(cards);
        }
    }

    private void ChangeOwner() {
        var newOwner = Players.Values.FirstOrDefault();
        if(newOwner == null) return;
        OwnerId = newOwner.Id;
        _hubClients.Group(Id).SendAsync("SetOwner", new {
            OwnerId,
            Players[OwnerId].Name,
        });
    }

    private void SetDecks(Dictionary<int, string> decks) {
        Decks = decks;
        BlackCards.Clear();
        WhiteCards.Clear();

        foreach(var deckId in decks.Keys) {
            var cards = _deckService.GetCards(deckId);
            BlackCards.AddRange(cards.Where(x => x.Type == CardType.Black));
            WhiteCards.AddRange(cards.Where(x => x.Type == CardType.White));
        }
        BlackCards = BlackCards.OrderBy(a => rng.Next()).ToList();
        WhiteCards = WhiteCards.OrderBy(a => rng.Next()).ToList();

        BlackCardsStack = new Queue<CardDto>(BlackCards);
        WhiteCardsStack = new Queue<CardDto>(WhiteCards);
    }

    public Game(IHubClients hubClients, IDeckService deckService, string id, string name, string owner, string? password, int maxPlayers, int maxRounds) {
        _hubClients = hubClients;
        _deckService = deckService;
        Id = id;
        Name = name;
        OwnerId = owner;
        Password = password;
        MaxPlayers = maxPlayers;
        MaxRounds = maxRounds;
    }

    public async Task Update(GameUpdateDto update) {
        if(update.Name != null) {
            Name = update.Name;
            await _hubClients.Group(Id).SendAsync("SetName", Name);
        }
        if(update.Password != null) {
            Password = update.Password;
        }
        if(update.OwnerId != null) {
            OwnerId = update.OwnerId;
            await _hubClients.Group(Id).SendAsync("SetOwner", new {
                OwnerId,
                Players[OwnerId].Name,
            });
        }
        if(update.MaxPlayers != null) {
            MaxPlayers = update.MaxPlayers.Value;
            await _hubClients.Group(Id).SendAsync("SetMaxPlayers", MaxPlayers);
        }
        if(update.MaxRounds != null) {
            MaxRounds = update.MaxRounds.Value;
            await _hubClients.Group(Id).SendAsync("SetMaxRounds", MaxRounds);
        }
        if(update.Decks != null) {
            SetDecks(update.Decks);
            await _hubClients.Group(Id).SendAsync("SetDecks", Decks);
        }
    }

    public void AddPlayer(string userId, string name) {
        if(Players.Count >= MaxPlayers) AddSpectator(userId, name);

        var player = new Player {
            Id = userId,
            Name = name,
        };
        Players[userId] = player;
        SendGameData(player.Id);
        _hubClients.Group(Id).SendAsync("AddPlayer", new {
            player.Id,
            player.Name,
        });
    }

    public void AddSpectator(string userId, string name) {
        var player = new Player {
            Id = userId,
            Name = name,
        };
        Spectators[userId] = player;
        SendGameData(player.Id);
        _hubClients.Group(Id).SendAsync("AddSpectator", new {
            player.Id,
            player.Name,
        });
    }

    public void RemoveUser(string userId) {
        Spectators.TryRemove(userId, out Player? spectator);
        if(spectator != null) {
            _hubClients.Group(Id).SendAsync("RemoveSpectator", new {
                spectator.Id,
                spectator.Name,
            });
        }

        Players.TryRemove(userId, out Player? player);
        if(player != null) { 
            _hubClients.Group(Id).SendAsync("RemovePlayer", new {
                player.Id,
                player.Name,
            });

            if(player.Id != OwnerId) return;
            ChangeOwner();
        }
    }

    public async Task StartGame() {
        WhiteCardsStack.OrderBy(a => rng.Next()).ToList();
        BlackCardsStack.OrderBy(a => rng.Next()).ToList();
        CardDto? blackCard = null;

        for(int i = 0; i < MaxRounds; i++) {
            CardCzar = new ConcurrentStack<Player>(Players.Values.OrderBy(a => rng.Next()).ToList());
            Round = i;

            while(CardCzar.TryPeek(out Player? czar)) {
                PlayedCards.Clear();
                await SendCards(Players.Values.ToArray());
                foreach(var player in Players.Values) {
                    player.IsReady = false;
                }
                await _hubClients.Group(Id).SendAsync("SetCardCzar", new {
                    czar.Id,
                    czar.Name,
                });
                try {
                    blackCard = BlackCardsStack.Dequeue();
                }
                catch(InvalidOperationException) {
                    BlackCardsStack = new Queue<CardDto>(BlackCards);
                    BlackCardsStack.OrderBy(a => rng.Next()).ToList();
                    blackCard = BlackCardsStack.Dequeue();
                }
                await _hubClients.Group(Id).SendAsync("SetBlackCard", blackCard);

                GameState = GameState.Question;
                await _hubClients.Group(Id).SendAsync("SetGameState", GameState);
                await Wait(20, Players.Values.Where(x => x.Id != czar.Id).ToArray());

                GameState = GameState.Answer;
                await _hubClients.Group(Id).SendAsync("SetGameState", GameState);
                await Wait(20, Players.Values.Where(x => x.Id == czar.Id).ToArray());

                CardCzar.TryPop(out Player? _);
            }
        }

        GameState = GameState.Lobby;
    }

    public async Task PlayCard(string userId, int cardId) {
        CardCzar.TryPeek(out Player? czar);
        var player = Players[userId];

        if(GameState != GameState.Question) return;
        if(czar == null) return;
        if(player == null) return;
        if(player.IsReady) return;
        if(player.Id == czar.Id) return;

        var card = player.Cards.FirstOrDefault(x => x.Id == cardId);
        if(card == null) return;

        player.Cards.Remove(card);
        PlayedCards.Push(new PlayedCard {
            PlayerId = player.Id,
            PlayerName = player.Name,
            CardId = card.Id,
        });
        player.IsReady = true;
        await _hubClients.Group(Id).SendAsync("PlayerReady", new {
            player.Id,
            player.Name,
        });
    }

    public async Task ChoseCard(string userId, int cardId) {
        CardCzar.TryPeek(out Player? czar);
        var player = Players[userId];

        if(GameState != GameState.Answer) return;
        if(czar == null) return;
        if(player == null) return;
        if(player.IsReady) return;
        if(player.Id != czar.Id) return;

        var card = PlayedCards.FirstOrDefault(x => x.CardId == cardId);
        if(card == null) return;

        Players[card.PlayerId].Score++;
        await _hubClients.Group(Id).SendAsync("SetChosenCard", new {
            player.Id,
            player.Name,
            cardId,
        });
    }

    private async Task Wait(int seconds, IEnumerable<Player> players) {
        NextState = DateTimeOffset.UtcNow.AddSeconds(seconds).ToUnixTimeSeconds();
        while(NextState > DateTimeOffset.UtcNow.ToUnixTimeSeconds() && !players.All(x => x.IsReady)) {
            await Task.Delay(1000);
        }
    }
}
