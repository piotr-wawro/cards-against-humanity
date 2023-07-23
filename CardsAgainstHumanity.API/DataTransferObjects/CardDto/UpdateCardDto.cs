using CardsAgainstHumanity.DatabaseAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace CardsAgainstHumanity.API.DataTransferObjects.CardDto;

public class UpdateCardDto
{
    [Required]
    public int Id { get; set; }
    public CardType? Type { get; set; }
    public string? Text { get; set; }
    public Language? Language { get; set; }
}