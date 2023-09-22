namespace CardsAgainstHumanity.API.DataTransferObjects.GameDto;

public class GameDto {
    public required string Id { get; set; }
    public required string[] Decks { get; set; }
    public required string[] Players { get; set; }
    public required string[] Spectators { get; set; }
}
