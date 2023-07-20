using CardsAgainstHumanity.DatabaseAccess.Entities;
using CardsAgainstHumanity.DatabaseAccess.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CardsAgainstHumanity.DatabaseAccess.DataAccess;

public class CahDbContext : DbContext {
    public DbSet<Audit> Audits { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardVote> CardVotes { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<DeckCard> DeckCards { get; set; }
    public DbSet<DeckGroup> DeckGroups { get; set; }
    public DbSet<DeckVote> DeckVotes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserHistory> UserHistories { get; set; }

    public CahDbContext(DbContextOptions<CahDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new AuditConfig());
        modelBuilder.ApplyConfiguration(new CardConfig());
        modelBuilder.ApplyConfiguration(new CardVoteConfig());
        modelBuilder.ApplyConfiguration(new DeckConfig());
        modelBuilder.ApplyConfiguration(new DeckGroupConfig());
        modelBuilder.ApplyConfiguration(new DeckVoteConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new UserHistoryConfig());
    }
}
