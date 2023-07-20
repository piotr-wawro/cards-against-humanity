using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class CardConfig : IEntityTypeConfiguration<Card> {
    public void Configure(EntityTypeBuilder<Card> builder) {
        builder
            .HasOne(child => child.User)
            .WithMany(parent => parent.Cards)
            .HasForeignKey(child => child.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(e => e.Text)
            .HasColumnType("nvarchar(64)");

        builder.Property(e => e.Language)
            .HasColumnType("nvarchar(32)")
            .HasConversion<string>(
                e => e.ToString(),
                e => (Language)Enum.Parse(typeof(Language), e)
            );

        builder.Property(e => e.Type)
            .HasColumnType("nchar(5)")
            .HasConversion<string>(
                e => e.ToString(),
                e => (CardType)Enum.Parse(typeof(CardType), e)
            );
    }
}
