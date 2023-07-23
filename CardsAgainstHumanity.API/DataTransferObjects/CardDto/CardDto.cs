using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.DataTransferObjects.CardDto;

public class CardDto
{
    public required int Id { get; set; }
    public required CardType Type { get; set; }
    public required string Text { get; set; }
    public required Language Language { get; set; }
    public required int UserId { get; set; }
    public required int? BaseCardId { get; set; }
    public required double? AverageVote { get; set; }
}
