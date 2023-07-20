using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

[Index(nameof(Nickname), IsUnique = true)]
public class User {
    public required int Id { get; set; }
    public required string Nickname { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required Byte Hash { get; set; }
    public required Byte Salt { get; set; }
    public required DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<Audit> Audits { get; } = new List<Audit>();
    public ICollection<UserHistory> UserHistories { get; } = new List<UserHistory>();
    public ICollection<DeckVote> DeckVotes { get; } = new List<DeckVote>();
    public ICollection<Deck> Decks { get; } = new List<Deck>();
    public ICollection<CardVote> CardVotes { get; } = new List<CardVote>();
    public ICollection<Card> Cards { get; } = new List<Card>();
    public ICollection<DeckGroup> DeckGroups { get; } = new List<DeckGroup>();
}