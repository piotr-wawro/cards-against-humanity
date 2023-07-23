using CardsAgainstHumanity.DatabaseAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CardsAgainstHumanity.API.DataTransferObjects.CardDto;

public class CardPageQueryParams
{
    [Required]
    public int PageNumber { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 30;
    public CardType? FilterType { get; set; } = null;
    public int? FilterUser { get; set; } = null;
    public Language? FilterLanguage { get; set; } = null;
    public bool? FilterIsBase { get; set; } = null;
    public CardOrder Order { get; set; } = CardOrder.Id;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardOrder {
    Id,
    Value,
}
