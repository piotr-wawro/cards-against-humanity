using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.DataTransferObjects.DeckDto;

public class CreateDeckDto {
    public required string Name { get; set; }
    public required Language Language { get; set; }
    public required bool SafeContent { get; set; }
}