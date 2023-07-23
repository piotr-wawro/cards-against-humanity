using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.DataTransferObjects.DeckDto;

public class DeckDto {
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required Language Language { get; set; }
    public required int black { get; set; }
    public required int white { get; set; }
    public required bool SafeContent { get; set; }
    public required double? AverageVote { get; set; }
    public required int UserId { get; set; }
}
