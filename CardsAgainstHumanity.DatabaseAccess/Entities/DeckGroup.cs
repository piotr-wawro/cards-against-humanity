namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class DeckGroup {
    public int Id { get; set; }
    public required string Name { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public required int DeckId { get; set; }
    public Deck? Deck { get; set; }
}
