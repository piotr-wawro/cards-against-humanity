using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.DataTransferObjects.DeckDto;

public class UpdateDeckDto {
    public int Id { get; set; }
    public string? Name { get; set; }
    public Language? Language { get; set; }
    public bool? SafeContent { get; set; }
    public int? UserId { get; set; }
}
