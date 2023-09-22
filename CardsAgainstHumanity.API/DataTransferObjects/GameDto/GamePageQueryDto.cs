using System.ComponentModel.DataAnnotations;

namespace CardsAgainstHumanity.API.DataTransferObjects.GameDto;

public class GamePageQueryDto {
    [Required]
    public int PageNumber { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 30;

}
