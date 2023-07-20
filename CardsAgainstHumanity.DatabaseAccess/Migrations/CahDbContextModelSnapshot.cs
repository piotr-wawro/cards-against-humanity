﻿// <auto-generated />
using System;
using CardsAgainstHumanity.DatabaseAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardsAgainstHumanity.DatabaseAccess.Migrations
{
    [DbContext(typeof(CahDbContext))]
    partial class CahDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.Property<int>("CardsId")
                        .HasColumnType("int");

                    b.Property<int>("DecksId")
                        .HasColumnType("int");

                    b.HasKey("CardsId", "DecksId");

                    b.HasIndex("DecksId");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Audit", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("AffectedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BaseCardId")
                        .HasColumnType("int");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nchar(5)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BaseCardId");

                    b.HasIndex("UserId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.CardVote", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<byte>("Vote")
                        .HasColumnType("tinyint");

                    b.HasKey("UserId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("CardVotes");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<short>("black")
                        .HasColumnType("smallint");

                    b.Property<bool>("safe_content")
                        .HasColumnType("bit");

                    b.Property<short>("white")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Name", "UserId")
                        .IsUnique();

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.DeckCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DeckCards");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.DeckGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.HasIndex("UserId");

                    b.ToTable("DeckGroups");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.DeckVote", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<byte>("Vote")
                        .HasColumnType("tinyint");

                    b.HasKey("UserId", "DeckId");

                    b.HasIndex("DeckId");

                    b.ToTable("DeckVotes");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("binary(32)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("binary(16)");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.UserHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("binary(32)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("binary(16)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserHistories");
                });

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", null)
                        .WithMany()
                        .HasForeignKey("DecksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Audit", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.UserHistory", "UserHistory")
                        .WithOne("Audit")
                        .HasForeignKey("CardsAgainstHumanity.DatabaseAccess.Entities.Audit", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("Audits")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserHistory");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Card", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Card", "BaseCard")
                        .WithMany("DerivedCards")
                        .HasForeignKey("BaseCardId");

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BaseCard");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.CardVote", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Card", "Card")
                        .WithMany("CardVotes")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("CardVotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("Decks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.DeckGroup", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", "Deck")
                        .WithMany("DeckGroups")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("DeckGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.DeckVote", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", "Deck")
                        .WithMany("DeckVotes")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("DeckVotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.UserHistory", b =>
                {
                    b.HasOne("CardsAgainstHumanity.DatabaseAccess.Entities.User", "User")
                        .WithMany("UserHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Card", b =>
                {
                    b.Navigation("CardVotes");

                    b.Navigation("DerivedCards");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.Deck", b =>
                {
                    b.Navigation("DeckGroups");

                    b.Navigation("DeckVotes");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.User", b =>
                {
                    b.Navigation("Audits");

                    b.Navigation("CardVotes");

                    b.Navigation("Cards");

                    b.Navigation("DeckGroups");

                    b.Navigation("DeckVotes");

                    b.Navigation("Decks");

                    b.Navigation("UserHistories");
                });

            modelBuilder.Entity("CardsAgainstHumanity.DatabaseAccess.Entities.UserHistory", b =>
                {
                    b.Navigation("Audit")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
