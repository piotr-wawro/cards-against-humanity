namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class Audit {
    public int Id { get; set; }
    public required DateTime AffectedOn { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public UserHistory? UserHistory { get; set; }
}
