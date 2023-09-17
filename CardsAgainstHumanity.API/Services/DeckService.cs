using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.API.DataTransferObjects.DeckDto;
using CardsAgainstHumanity.API.Middleware.ErrorHandlers;
using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using CardsAgainstHumanity.DatabaseAccess.Entities;

namespace CardsAgainstHumanity.API.Services;

public class DeckService : IDeckService {
    private readonly CahDbContext _context;

    public DeckService(CahDbContext context) {
        _context = context;
    }

    public DeckDto? GetDeck(int id) {
        var deck =
            from d in _context.Decks
            where d.Id == id
            select new DeckDto {
                Id = d.Id,
                Name = d.Name,
                Language = d.Language,
                black = d.black,
                white = d.white,
                SafeContent = d.SafeContent,
                AverageVote = null,
                UserId = d.UserId
            };

        return deck.FirstOrDefault();
    }

    public IEnumerable<DeckDto> GetPage(DeckPageQueryParams par) {
        var decks =
            from d in _context.Decks
            join dv in _context.DeckVotes on d.Id equals dv.DeckId into tmp
            from dv in tmp.DefaultIfEmpty()
            group new { d, dv }
            by new { d.Id, d.Name, d.Language, d.black, d.white, d.SafeContent, d.UserId } into g
            select new DeckDto {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Language = g.Key.Language,
                black = g.Key.black,
                white = g.Key.white,
                SafeContent = g.Key.SafeContent,
                AverageVote = g.Average(x => x.dv.Vote),
                UserId = g.Key.UserId
            };

        if(par.FilterUser != null) {
            decks =
                from d in decks
                where d.UserId == par.FilterUser
                select d;
        }
        if(par.FilterLanguage != null) {
            decks =
                from d in decks
                where d.Language == par.FilterLanguage
                select d;
        }

        if(par.Order == CardOrder.Id) {
            decks =
                from d in decks
                orderby d.Id descending
                where d.Id > par.PageSize * (par.PageNumber - 1)
                where d.Id <= par.PageSize * par.PageNumber
                select d;
        }
        else {
            decks =
                (from d in decks
                 orderby d.AverageVote descending
                 select d)
                .Skip(par.PageSize * (par.PageNumber - 1)).Take(par.PageSize);
        }

        return decks.ToList();
    }

    public DeckDto CreateDeck(int userId, CreateDeckDto par) {
        var deck = new Deck {
            Name = par.Name,
            Language = par.Language,
            black = 0,
            white = 0,
            SafeContent = par.SafeContent,
            UserId = userId
        };

        _context.Decks.Add(deck);
        _context.SaveChanges();

        return new DeckDto {
            Id = deck.Id,
            Name = deck.Name,
            Language = deck.Language,
            black = deck.black,
            white = deck.white,
            SafeContent = deck.SafeContent,
            AverageVote = null,
            UserId = deck.UserId
        };
    }

    public DeckDto UpdateDeck(int userId, UpdateDeckDto par) {
        var deck =
            (from d in _context.Decks
            where d.Id == par.Id
            select d).FirstOrDefault();

        if(deck == null) {
            throw ApiError.NotFound("Deck not found");
        }

        if(deck.UserId != userId) {
            throw ApiError.Forbidden("Cannot update deck of another user");
        }

        if(par.Name != null) {
            deck.Name = par.Name;
        }
        if(par.Language != null) {
            deck.Language = par.Language.Value;
        }
        if(par.SafeContent != null) {
            deck.SafeContent = (bool)par.SafeContent;
        }

        _context.SaveChanges();

        return new DeckDto {
            Id = deck.Id,
            Name = deck.Name,
            Language = deck.Language,
            black = deck.black,
            white = deck.white,
            SafeContent = deck.SafeContent,
            AverageVote = null,
            UserId = deck.UserId
        };
    }

    public void DeleteDeck(int userId, int id) {
        var deck =
            (from d in _context.Decks
            where d.Id == id
            select d).FirstOrDefault();

        if(deck == null) {
            throw ApiError.NotFound("Deck not found");
        }

        if(deck.UserId != userId) {
            throw ApiError.Forbidden("Cannot delete deck of another user");
        }

        _context.Decks.Remove(deck);
        _context.SaveChanges();
    }

    public IEnumerable<CardDto> GetCards(int id) {
        var cards =
            from c in _context.Cards
            join dc in _context.DeckCards on c.Id equals dc.CardId
            where dc.DeckId == id
            select new CardDto {
                Id = c.Id,
                Type = c.Type,
                Text = c.Text,
                Language = c.Language,
                UserId = c.UserId,
                BaseCardId = c.BaseCardId,
                AverageVote = null,
            };

        return cards.ToList();
    }
}
