using Microsoft.EntityFrameworkCore;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

[PrimaryKey(nameof(UserId), nameof(DeckId))]
public class DeckVote {
    public required int Vote { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public required int DeckId { get; set; }
    public Deck? Deck { get; set; }
}
