using Microsoft.EntityFrameworkCore;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

[PrimaryKey(nameof(UserId), nameof(CardId))]
public class CardVote {
    public required int Vote { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public required int CardId { get; set; }
    public Card? Card { get; set; }
}
