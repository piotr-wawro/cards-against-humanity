using CardsAgainstHumanity.API.DataTransferObjects.GameDto;

namespace CardsAgainstHumanity.API.Services;

public interface IGameService {
    Game GetGame(string gameId);
    IEnumerable<GameDto> GetPage(GamePageQueryDto par);
    void CreateGame(string gameId, string name, string ownerId, string? pass, int rounds);
    void DeleteGame(string id);
}
