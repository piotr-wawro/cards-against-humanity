using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;

public class DeckGroupConfig : IEntityTypeConfiguration<DeckGroup> {
    public void Configure(EntityTypeBuilder<DeckGroup> builder) {
        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(64)");
    }
}
