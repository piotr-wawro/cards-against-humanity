using System.Text.Json.Serialization;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class Card {
    public int Id { get; set; }
    public required CardType Type { get; set; }
    public required string Text { get; set; }
    public required Language Language { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public int? BaseCardId { get; set; }
    public Card? BaseCard { get; set; }
    public ICollection<Card> DerivedCards { get; } = new List<Card>();

    public ICollection<CardVote> CardVotes { get; } = new List<CardVote>();
    public ICollection<Deck> Decks { get; } = new List<Deck>();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardType {
    White,
    Black
}
