namespace CardsAgainstHumanity.API.DataTransferObjects.GameDto;

public class GameUpdateDto {
    public string? Name { get; set; }
    public string? OwnerId { get; set; }
    public string? Password { get; set; }
    public int? MaxPlayers { get; set; }
    public int? MaxRounds { get; set; }
    public Dictionary<int, string>? Decks { get; set; }
}
