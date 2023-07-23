using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardsAgainstHumanity.DevelopmentMigrations.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeckCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "varchar(32)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Hash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nchar(5)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BaseCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Cards_BaseCardId",
                        column: x => x.BaseCardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    black = table.Column<short>(type: "smallint", nullable: false),
                    white = table.Column<short>(type: "smallint", nullable: false),
                    SafeContent = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    Hash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardVotes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardVotes", x => new { x.UserId, x.CardId });
                    table.ForeignKey(
                        name: "FK_CardVotes_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardDeck",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "int", nullable: false),
                    DecksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDeck", x => new { x.CardsId, x.DecksId });
                    table.ForeignKey(
                        name: "FK_CardDeck_Cards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDeck_Decks_DecksId",
                        column: x => x.DecksId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeckGroups_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckVotes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    Vote = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckVotes", x => new { x.UserId, x.DeckId });
                    table.ForeignKey(
                        name: "FK_DeckVotes_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AffectedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audits_UserHistories_Id",
                        column: x => x.Id,
                        principalTable: "UserHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Audits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Created", "Deleted", "Email", "Hash", "Nickname", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user1@xyz.com", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 }, "User1", "User", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 } },
                    { 2, new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user2@xyz.com", new byte[] { 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34 }, "User2", "User", new byte[] { 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34 } }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "BaseCardId", "Language", "Text", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, null, "English", "White card 1", "White", 1 },
                    { 2, null, "English", "White card 2", "White", 1 },
                    { 3, null, "English", "White card 3", "White", 2 },
                    { 5, null, "English", "Black card 1", "Black", 2 }
                });

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "Id", "Language", "Name", "SafeContent", "UserId", "black", "white" },
                values: new object[,]
                {
                    { 1, "English", "Deck 1", true, 1, (short)1, (short)1 },
                    { 2, "English", "Deck 2", true, 2, (short)1, (short)1 }
                });

            migrationBuilder.InsertData(
                table: "UserHistories",
                columns: new[] { "Id", "Deleted", "Email", "Hash", "Nickname", "Salt", "UserId" },
                values: new object[,]
                {
                    { 1, null, "user1@xyz.com", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 }, "User111", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 }, 1 },
                    { 2, null, "user2@xyz.com", new byte[] { 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34 }, "User22", new byte[] { 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34, 34 }, 2 },
                    { 3, null, "user1@xyz.com", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 }, "User11", new byte[] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 }, 1 }
                });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AffectedOn", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "CardVotes",
                columns: new[] { "CardId", "UserId", "Vote" },
                values: new object[,]
                {
                    { 1, 1, (byte)1 },
                    { 2, 1, (byte)1 },
                    { 2, 2, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "BaseCardId", "Language", "Text", "Type", "UserId" },
                values: new object[] { 4, 2, "English", "White card 4", "White", 2 });

            migrationBuilder.InsertData(
                table: "DeckGroups",
                columns: new[] { "Id", "DeckId", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "User 1 deck group", 1 },
                    { 2, 1, "User 2 deck group", 2 }
                });

            migrationBuilder.InsertData(
                table: "DeckVotes",
                columns: new[] { "DeckId", "UserId", "Vote" },
                values: new object[] { 1, 1, (byte)1 });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_UserId",
                table: "Audits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_DecksId",
                table: "CardDeck",
                column: "DecksId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BaseCardId",
                table: "Cards",
                column: "BaseCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardVotes_CardId",
                table: "CardVotes",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckGroups_DeckId",
                table: "DeckGroups",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckGroups_UserId",
                table: "DeckGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_Name_UserId",
                table: "Decks",
                columns: new[] { "Name", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserId",
                table: "Decks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckVotes_DeckId",
                table: "DeckVotes",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_UserId",
                table: "UserHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Nickname",
                table: "Users",
                column: "Nickname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "CardVotes");

            migrationBuilder.DropTable(
                name: "DeckCards");

            migrationBuilder.DropTable(
                name: "DeckGroups");

            migrationBuilder.DropTable(
                name: "DeckVotes");

            migrationBuilder.DropTable(
                name: "UserHistories");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
