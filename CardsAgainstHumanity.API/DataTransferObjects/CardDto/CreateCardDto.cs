using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.DataTransferObjects.CardDto;

public class CreateCardDto
{
    public CardType Type { get; set; }
    public string Text { get; set; } = null!;
    public Language Language { get; set; }
    public int? BaseCardId { get; set; }
}
