using CardsAgainstHumanity.API.DataTransferObjects.CardDto;

namespace CardsAgainstHumanity.API.Services;
public interface ICardService {
    CardDto? GetCard(int id);
    IEnumerable<CardDto> GetPage(CardPageQueryParams par);
    CardDto CreateCard(int userId, CreateCardDto par);
    CardDto UpdateCard(int userId, UpdateCardDto par);
    void DeleteCard(int userId, int id);
}