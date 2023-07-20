namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class Audit {
    public required int Id { get; set; }
    public required DateTime AffectedOn { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public required UserHistory UserHistory { get; set; }
}
