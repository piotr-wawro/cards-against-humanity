namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class DeckCard {
    public required int Id { get; set; }
    public required int DeckId { get; set; }
    public required int CardId { get; set; }
}
