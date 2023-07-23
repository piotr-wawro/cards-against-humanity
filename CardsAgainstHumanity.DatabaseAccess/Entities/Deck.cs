using Microsoft.EntityFrameworkCore;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

[Index(nameof(Name), nameof(UserId), IsUnique = true)]
public class Deck {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Language Language { get; set; }
    public required int black { get; set; }
    public required int white { get; set; }
    public required bool SafeContent { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<DeckGroup> DeckGroups { get; set; } = new List<DeckGroup>();
    public ICollection<DeckVote> DeckVotes { get; } = new List<DeckVote>();
    public ICollection<Card> Cards { get; } = new List<Card>();
}
