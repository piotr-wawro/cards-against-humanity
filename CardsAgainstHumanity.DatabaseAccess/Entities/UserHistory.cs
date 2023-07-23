using System.ComponentModel.DataAnnotations;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;

public class UserHistory {
    public int Id { get; set; }
    public required string Nickname { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    public required Byte[] Hash { get; set; }
    public required Byte[] Salt { get; set; }
    public DateTime? Deleted { get; set; }

    public required int UserId { get; set; }
    public User? User { get; set; }

    public Audit? Audit { get; set; }
}
