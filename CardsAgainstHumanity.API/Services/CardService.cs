using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using CardsAgainstHumanity.DatabaseAccess.Entities;
using CardsAgainstHumanity.API.Middleware.ErrorHandlers;
using CardsAgainstHumanity.API.DataTransferObjects.CardDto;

namespace CardsAgainstHumanity.API.Services;

public class CardService : ICardService {
    private readonly CahDbContext _context;

    public CardService(CahDbContext context) {
        _context = context;
    }
    public CardDto? GetCard(int id) {
        IQueryable<CardDto> queryCard =
            from card in _context.Cards
            join cardVote in _context.CardVotes on card.Id equals cardVote.CardId into cardVotes
            where card.Id == id
            select new CardDto() {
                Id = card.Id,
                Type = card.Type,
                Text = card.Text,
                Language = card.Language,
                UserId = card.UserId,
                BaseCardId = card.BaseCardId,
                AverageVote = cardVotes.Average(cardVote => cardVote.Vote),
            };

        return queryCard.FirstOrDefault();
    }

    public IEnumerable<CardDto> GetPage(CardPageQueryParams par) {
        IQueryable<CardDto> queryCard =
            from card in _context.Cards
            join cardVote in _context.CardVotes on card.Id equals cardVote.CardId into tmp
            from cardVote in tmp.DefaultIfEmpty()
            group new { card, cardVote }
            by new { card.Id, card.Type, card.Text, card.Language, card.UserId, card.BaseCardId } into g
            select new CardDto() {
                Id = g.Key.Id,
                Type = g.Key.Type,
                Text = g.Key.Text,
                Language = g.Key.Language,
                UserId = g.Key.UserId,
                BaseCardId = g.Key.BaseCardId,
                AverageVote = g.Average(x => x.cardVote.Vote)
            };

        if(par.FilterType != null) {
            queryCard =
                from card in queryCard
                where card.Type == par.FilterType
                select card;
        }
        if(par.FilterUser != null) {
            queryCard =
                from card in queryCard
                where card.UserId == par.FilterUser
                select card;
        }
        if(par.FilterLanguage != null) {
            queryCard =
                from card in queryCard
                where card.Language == par.FilterLanguage
                select card;
        }
        if(par.FilterIsBase != null) {
            if(par.FilterIsBase == true) {
                queryCard =
                    from card in queryCard
                    where card.BaseCardId == null
                    select card;
            }
            else {
                queryCard =
                    from card in queryCard
                    where card.BaseCardId != null
                    select card;
            }
        }

        if(par.Order == CardOrder.Id) {
            queryCard =
                from card in queryCard
                orderby card.Id descending
                where card.Id > par.PageSize * (par.PageNumber - 1)
                where card.Id <= par.PageSize * par.PageNumber
                select card;
        }
        else {
            queryCard =
                (from card in queryCard
                 orderby card.AverageVote descending
                 select card)
                .Skip(par.PageSize * (par.PageNumber - 1)).Take(par.PageSize);
        }

        return queryCard.ToList();
    }

    public CardDto CreateCard(int userId, CreateCardDto par) {
        var card = _context.Cards.Add(new Card() {
            Type = par.Type,
            Text = par.Text,
            Language = par.Language,
            UserId = userId,
            BaseCardId = par.BaseCardId,
        });

        _context.SaveChanges();

        var cardDto = new CardDto() {
            Id = card.Entity.Id,
            Type = card.Entity.Type,
            Text = card.Entity.Text,
            Language = card.Entity.Language,
            UserId = card.Entity.UserId,
            BaseCardId = card.Entity.BaseCardId,
            AverageVote = null,
        };

        return cardDto;
    }

    public CardDto UpdateCard(int userId, UpdateCardDto par) {
        var card = 
            (from c in _context.Cards
            where c.Id == par.Id
            select c).FirstOrDefault();

        if(card == null) {
            throw ApiError.NotFound("Card not found");
        }

        if(card.UserId != userId) {
            throw ApiError.Forbidden("Cannot update card of another user");
        }

        var voteCount =
            from cardVote in _context.CardVotes
            where cardVote.CardId == par.Id
            select cardVote;

        if(voteCount.Count() > 0) {
            throw ApiError.Conflict("Cannot update card with votes");
        }

        var deckCount =
            from deckCard in _context.DeckCards
            where deckCard.CardId == par.Id
            select deckCard;

        if(deckCount.Count() > 0) {
            throw ApiError.Conflict("Cannot update card with deck");
        }

        if(par.Type != null) {
            card.Type = par.Type.Value;
        }
        if(par.Text != null) {
            card.Text = par.Text;
        }
        if(par.Language != null) {
            card.Language = par.Language.Value;
        }

        _context.SaveChanges();
        return new CardDto() {
            Id = card.Id,
            Type = card.Type,
            Text = card.Text,
            Language = card.Language,
            UserId = card.UserId,
            BaseCardId = card.BaseCardId,
            AverageVote = null,
        };
    }

    public void DeleteCard(int UserId, int id) {
        var card =
            (from c in _context.Cards
            where c.Id == id
            select c).FirstOrDefault();

        if(card == null) {
            throw ApiError.NotFound("Card not found");
        }

        if(card.UserId != UserId) {
            throw ApiError.Forbidden("Cannot delete card of another user");
        }

        var voteCount =
            from cardVote in _context.CardVotes
            where cardVote.CardId == id
            select cardVote;

        if(voteCount.Count() > 0) {
            throw ApiError.Conflict("Cannot delete card with votes");
        }

        var deckCount =
            from deckCard in _context.DeckCards
            where deckCard.CardId == id
            select deckCard;

        if(deckCount.Count() > 0) {
            throw ApiError.Conflict("Cannot delete card with deck");
        }

        _context.Cards.Remove(card);
        _context.SaveChanges();
    }
}
