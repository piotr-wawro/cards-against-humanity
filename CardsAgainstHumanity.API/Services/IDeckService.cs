using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.API.DataTransferObjects.DeckDto;

namespace CardsAgainstHumanity.API.Services;

public interface IDeckService {
    DeckDto? GetDeck(int id);
    IEnumerable<DeckDto> GetPage(DeckPageQueryParams par);
    DeckDto CreateDeck(int userId, CreateDeckDto par);
    DeckDto UpdateDeck(int userId, UpdateDeckDto par);
    void DeleteDeck(int userId, int id);
    IEnumerable<CardDto> GetCards(int id);
}
