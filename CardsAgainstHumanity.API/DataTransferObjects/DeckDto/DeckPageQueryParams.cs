using CardsAgainstHumanity.API.DataTransferObjects.CardDto;
using CardsAgainstHumanity.DatabaseAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace CardsAgainstHumanity.API.DataTransferObjects.DeckDto;

public class DeckPageQueryParams
{
    [Required]
    public int PageNumber { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 30;
    public int? FilterUser { get; set; } = null;
    public Language? FilterLanguage { get; set; } = null;
    public CardOrder Order { get; set; } = CardOrder.Id;
}
