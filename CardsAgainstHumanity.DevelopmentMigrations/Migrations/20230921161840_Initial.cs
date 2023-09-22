using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardsAgainstHumanity.DevelopmentMigrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Text = table.Column<string>(type: "nvarchar(256)", nullable: false),
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
                    { 1, null, "English", "____? There's an app for that", "Black", 1 },
                    { 2, null, "English", "Why can't I sleep at night? ____", "Black", 1 },
                    { 3, null, "English", "What's that smell? ____", "Black", 1 },
                    { 4, null, "English", "I got 99 problems but ____ ain't one.", "Black", 1 },
                    { 5, null, "English", "Who stole the cookies from the cookie jar? ____", "Black", 1 },
                    { 6, null, "English", "What's the next Happy Meal (r) toy? ____", "Black", 1 },
                    { 7, null, "English", "Anthropologists have recently discovered a primitive tribe that worships ____.", "Black", 1 },
                    { 8, null, "English", "It's a pity that kids these days are all getting involved with ____.", "Black", 1 },
                    { 9, null, "English", "During Picasso's often-overlooked Brown Period, he produced hundreds of paintings of ____.", "Black", 1 },
                    { 10, null, "English", "Alternative medicine is now embracing the curative powers of ____.", "Black", 1 },
                    { 11, null, "English", "And the Academy Award for ____ goes to ____.", "Black", 1 },
                    { 12, null, "English", "What's that sound? ____", "Black", 1 },
                    { 13, null, "English", "What ended my last relationship? ____", "Black", 1 },
                    { 14, null, "English", "MTV's new reality TV show features eight washed-up celebrities living with ____.", "Black", 1 },
                    { 15, null, "English", "I drink to forget ____.", "Black", 1 },
                    { 16, null, "English", "I'm sorry, I couldn't complete my homework because of ____.", "Black", 1 },
                    { 17, null, "English", "What is Batman's guilty pleasure? ____", "Black", 1 },
                    { 18, null, "English", "This is the way the world ends. Not with a bang but with ____.", "Black", 1 },
                    { 19, null, "English", "What's a girl's best friend? ____", "Black", 1 },
                    { 20, null, "English", "TSA guidelines now prohibit ____ on airplanes.", "Black", 1 },
                    { 21, null, "English", "____. That's how I want to die.", "Black", 1 },
                    { 22, null, "English", "For my next trick, I will pull ____ out of ____.", "Black", 1 },
                    { 23, null, "English", "In the new Disney Channel Original Movie, Hannah Montana struggles with ____ for the first time.", "Black", 1 },
                    { 24, null, "English", "____ is a slippery slope that leads to ____.", "Black", 1 },
                    { 25, null, "English", "What does Dick Cheney prefer? ____", "Black", 1 },
                    { 26, null, "English", "I wish I hadn't lost the instruction manual for ____.", "Black", 1 },
                    { 27, null, "English", "Instead of coal, Santa now gives the bad children ____.", "Black", 1 },
                    { 28, null, "English", "What's the most emo? ____", "Black", 1 },
                    { 29, null, "English", "In 1,000 years, when paper money is but a distant memory, ____ will be our currency.", "Black", 1 },
                    { 30, null, "English", "What's the next superhero/sidekick duo? ____ ____", "Black", 1 },
                    { 31, null, "English", "In M. Night Shyamalan's new movie, Bruce Willis discovers that ____ had really been ____ all along", "Black", 1 },
                    { 32, null, "English", "A romantic, candlelit dinner would be incomplete without ____.", "Black", 1 },
                    { 33, null, "English", "____. Betcha can't have just one!", "Black", 1 },
                    { 34, null, "English", "White people like ____.", "Black", 1 },
                    { 35, null, "English", "____. High five, bro.", "Black", 1 },
                    { 36, null, "English", "Next from J.K. Rowling: Harry Potter and Chamber of ____.", "Black", 1 },
                    { 37, null, "English", "BILLY MAYS HERE FOR ____.", "Black", 1 },
                    { 38, null, "English", "In a world ravaged by ____, our only solace is ____.", "Black", 1 },
                    { 39, null, "English", "War! What is it good for? ____", "Black", 1 },
                    { 40, null, "English", "During sex, I like to think about ____.", "Black", 1 },
                    { 41, null, "English", "What are my parents hiding from me? ____", "Black", 1 },
                    { 42, null, "English", "What will always get you laid? ____", "Black", 1 },
                    { 43, null, "English", "When I'm in prison, I'll have ____ smuggled in.", "Black", 1 },
                    { 44, null, "English", "What did I bring back from Mexico? ____", "Black", 1 },
                    { 45, null, "English", "What don't you want to find in your Chinese food? ____", "Black", 1 },
                    { 46, null, "English", "What will I bring back in time to convince people that I am a powerful wizard? ____", "Black", 1 },
                    { 47, null, "English", "How am I maintaining my relationship status? ____", "Black", 1 },
                    { 48, null, "English", "Coming to Broadway this season, ____: The Musical.", "Black", 1 },
                    { 49, null, "English", "While the United States raced the Soviet Union to the moon, the Mexican government funneled millions of pesos into research on ____.", "Black", 1 },
                    { 50, null, "English", "After Hurricane Katrina, Sean Penn brought ____ to the people of New Orleans.", "Black", 1 },
                    { 51, null, "English", "Due to a PR fiasco, Walmart no longer offers ____.", "Black", 1 },
                    { 52, null, "English", "In his new summer comedy, Rob Schneider is ____ trapped in the body of ____.", "Black", 1 },
                    { 53, null, "English", "Rumor has it that Vladimir Putin's favorite dish is ____ stuffed with ____.", "Black", 1 },
                    { 54, null, "English", "But before I kill you Mr. Bond, I must show you ____.", "Black", 1 },
                    { 55, null, "English", "What gives me uncontrollable gas? ____", "Black", 1 },
                    { 56, null, "English", "What do old people smell like? ____", "Black", 1 },
                    { 57, null, "English", "The class field trip was completely ruined by ____.", "Black", 1 },
                    { 58, null, "English", "When Pharaoh remained unmoved, Moses called down a Plague of ____.", "Black", 1 },
                    { 59, null, "English", "What's my secret power? ____", "Black", 1 },
                    { 60, null, "English", "what's there a ton of in heaven? ____", "Black", 1 },
                    { 61, null, "English", "What would grandma find disturbing, yet oddly charming? ____", "Black", 1 },
                    { 62, null, "English", "I never truly understood ____ until I encountered ____.", "Black", 1 },
                    { 63, null, "English", "The US has begun airdropping ____ to the children of Afghanistan.", "Black", 1 },
                    { 64, null, "English", "What helps Obama unwind? ____", "Black", 1 },
                    { 65, null, "English", "What did Vin Diesel eat for dinner? ____", "Black", 1 },
                    { 66, null, "English", "____: good to the last drop.", "Black", 1 },
                    { 67, null, "English", "Why am I sticky? ____", "Black", 1 },
                    { 68, null, "English", "What gets better with age? ____", "Black", 1 },
                    { 69, null, "English", "____: kid-tested, mother-approved.", "Black", 1 },
                    { 70, null, "English", "What's the crustiest? ____", "Black", 1 },
                    { 71, null, "English", "What's Teach for America using to inspire inner city students to succeed? ____", "Black", 1 },
                    { 72, null, "English", "Studies show that lab rats navigate mazes 50% faster after being exposed to ____.", "Black", 1 },
                    { 73, null, "English", "Life was difficult for cavemen before ____.", "Black", 1 },
                    { 74, null, "English", "Make a haiku. ____ ____ ____", "Black", 1 },
                    { 75, null, "English", "I do not know with what weapons World War III will be fought, but World War IV will be fought with ____.", "Black", 1 },
                    { 76, null, "English", "Why do I hurt all over? ____", "Black", 1 },
                    { 77, null, "English", "What am I giving up for Lent? ____", "Black", 1 },
                    { 78, null, "English", "In Michael Jackson's final moments, he thought about ____.", "Black", 1 },
                    { 79, null, "English", "In an attempt to reach a wider audience, the Smithsonian Museum of Natural History has opened an interactive exhibit on ____.", "Black", 1 },
                    { 80, null, "English", "When I am President of the United States, I will create the department of ____.", "Black", 1 },
                    { 81, null, "English", "Lifetime (r) presents ____, the story of ____.", "Black", 1 },
                    { 82, null, "English", "When I am a billionaire, I shall erect a 50-foot statue to commemorate ____.", "Black", 1 },
                    { 83, null, "English", "When I was tripping on acid, ____ turned into ____.", "Black", 1 },
                    { 84, null, "English", "That's right, I killed ____. How, you ask? ____.", "Black", 1 },
                    { 85, null, "English", "What's my anti-drug? ____", "Black", 1 },
                    { 86, null, "English", "____ + ____ = ____.", "Black", 1 },
                    { 87, null, "English", "What never fails to liven up the party? ____", "Black", 1 },
                    { 88, null, "English", "What's the new fad diet? ____", "Black", 1 },
                    { 89, null, "English", "Major League Baseball has banned ____ for giving players an unfair advantage.", "Black", 1 },
                    { 90, null, "English", "Coat hanger abortions", "White", 1 },
                    { 91, null, "English", "Man meat", "White", 1 },
                    { 92, null, "English", "Autocannibalism", "White", 1 },
                    { 93, null, "English", "Vigorous jazz hands", "White", 1 },
                    { 94, null, "English", "Flightless birds", "White", 1 },
                    { 95, null, "English", "Pictures of boobs", "White", 1 },
                    { 96, null, "English", "Doing the right thing", "White", 1 },
                    { 97, null, "English", "Hunting accidents", "White", 1 },
                    { 98, null, "English", "A cartoon camel enjoying the smooth, refreshing taste of a cigarette", "White", 1 },
                    { 99, null, "English", "The violation of our most basic human rights", "White", 1 },
                    { 100, null, "English", "Viagra (r)", "White", 1 },
                    { 101, null, "English", "Self-loathing", "White", 1 },
                    { 102, null, "English", "Spectacular abs", "White", 1 },
                    { 103, null, "English", "An honest cop with nothing left to lose", "White", 1 },
                    { 104, null, "English", "Abstinence", "White", 1 },
                    { 105, null, "English", "A balanced breakfast", "White", 1 },
                    { 106, null, "English", "Mountain Dew Code Red", "White", 1 },
                    { 107, null, "English", "Concealing a boner", "White", 1 },
                    { 108, null, "English", "Roofies", "White", 1 },
                    { 109, null, "English", "Glenn Beck convulsively vomiting as a brood of crab spiders hatches in his brain and erupts from his tear ducts", "White", 1 },
                    { 110, null, "English", "Tweeting", "White", 1 },
                    { 111, null, "English", "The Big Bang", "White", 1 },
                    { 112, null, "English", "Amputees", "White", 1 },
                    { 113, null, "English", "Dr. Martin Luther King, Jr.", "White", 1 },
                    { 114, null, "English", "Former President George W. Bush", "White", 1 },
                    { 115, null, "English", "Being marginalized", "White", 1 },
                    { 116, null, "English", "Smegma", "White", 1 },
                    { 117, null, "English", "Laying an egg", "White", 1 },
                    { 118, null, "English", "Cuddling", "White", 1 },
                    { 119, null, "English", "Aaron Burr", "White", 1 },
                    { 120, null, "English", "The Pope", "White", 1 },
                    { 121, null, "English", "A bleached asshole", "White", 1 },
                    { 122, null, "English", "Horse meat", "White", 1 },
                    { 123, null, "English", "Genital piercings", "White", 1 },
                    { 124, null, "English", "Fingering", "White", 1 },
                    { 125, null, "English", "Elderly Japanese men", "White", 1 },
                    { 126, null, "English", "Stranger danger", "White", 1 },
                    { 127, null, "English", "Fear itself", "White", 1 },
                    { 128, null, "English", "Science", "White", 1 },
                    { 129, null, "English", "Praying the gay away", "White", 1 },
                    { 130, null, "English", "Same-sex ice dancing", "White", 1 },
                    { 131, null, "English", "The terrorists", "White", 1 },
                    { 132, null, "English", "Making sex at here", "White", 1 },
                    { 133, null, "English", "German dungeon porn", "White", 1 },
                    { 134, null, "English", "Bingeing and purging", "White", 1 },
                    { 135, null, "English", "Ethnic cleansing", "White", 1 },
                    { 136, null, "English", "Cheating in the Special Olympics", "White", 1 },
                    { 137, null, "English", "Nickelback", "White", 1 },
                    { 138, null, "English", "Heteronormativity", "White", 1 },
                    { 139, null, "English", "William Shatner", "White", 1 },
                    { 140, null, "English", "Making a pouty face", "White", 1 },
                    { 141, null, "English", "Chainsaws for hands", "White", 1 },
                    { 142, null, "English", "The placenta", "White", 1 },
                    { 143, null, "English", "The profoundly handicapped", "White", 1 },
                    { 144, null, "English", "Tom Cruise", "White", 1 },
                    { 145, null, "English", "Object permanence", "White", 1 },
                    { 146, null, "English", "Goblins", "White", 1 },
                    { 147, null, "English", "An icepick lobotomy", "White", 1 },
                    { 148, null, "English", "Arnold Schwarzenegger", "White", 1 },
                    { 149, null, "English", "Hormone injections", "White", 1 },
                    { 150, null, "English", "A falcon with a cap on its head", "White", 1 },
                    { 151, null, "English", "Foreskin", "White", 1 },
                    { 152, null, "English", "Dying", "White", 1 },
                    { 153, null, "English", "Stunt doubles", "White", 1 },
                    { 154, null, "English", "The invisible hand", "White", 1 },
                    { 155, null, "English", "Jew-fros", "White", 1 },
                    { 156, null, "English", "A really cool hat", "White", 1 },
                    { 157, null, "English", "Flash flooding", "White", 1 },
                    { 158, null, "English", "Flavored condoms", "White", 1 },
                    { 159, null, "English", "Dying of dysyntery", "White", 1 },
                    { 160, null, "English", "Sexy pillow fights", "White", 1 },
                    { 161, null, "English", "The Three-Fifths compromise", "White", 1 },
                    { 162, null, "English", "A sad handjob", "White", 1 },
                    { 163, null, "English", "Men", "White", 1 },
                    { 164, null, "English", "Historically black colleges", "White", 1 },
                    { 165, null, "English", "Sean Penn", "White", 1 },
                    { 166, null, "English", "Heartwarming orphans", "White", 1 },
                    { 167, null, "English", "Waterboarding", "White", 1 },
                    { 168, null, "English", "The clitoris", "White", 1 },
                    { 169, null, "English", "Vikings", "White", 1 },
                    { 170, null, "English", "Friends who eat all the snacks", "White", 1 },
                    { 171, null, "English", "The Underground Railroad", "White", 1 },
                    { 172, null, "English", "Pretending to care", "White", 1 },
                    { 173, null, "English", "Raptor attacks", "White", 1 },
                    { 174, null, "English", "A micropenis", "White", 1 },
                    { 175, null, "English", "A Gypsy curse", "White", 1 },
                    { 176, null, "English", "Agriculture", "White", 1 },
                    { 177, null, "English", "Bling", "White", 1 },
                    { 178, null, "English", "A clandestine butt scratch", "White", 1 },
                    { 179, null, "English", "The South", "White", 1 },
                    { 180, null, "English", "Sniffing glue", "White", 1 },
                    { 181, null, "English", "Consultants", "White", 1 },
                    { 182, null, "English", "My humps", "White", 1 },
                    { 183, null, "English", "Geese", "White", 1 },
                    { 184, null, "English", "Being a dick to children", "White", 1 },
                    { 185, null, "English", "Party poopers", "White", 1 },
                    { 186, null, "English", "Sunshine and rainbows", "White", 1 },
                    { 187, null, "English", "YOU MUST CONSTRUCT ADDITIONAL PYLONS", "White", 1 },
                    { 188, null, "English", "Mutually-assured destruction", "White", 1 },
                    { 189, null, "English", "Heath Ledger", "White", 1 },
                    { 190, null, "English", "Sexting", "White", 1 },
                    { 191, null, "English", "An Oedipus complex", "White", 1 },
                    { 192, null, "English", "Eating all of the cookies before the AIDS bake-sale", "White", 1 },
                    { 193, null, "English", "A sausage festival", "White", 1 },
                    { 194, null, "English", "Michael Jackson", "White", 1 },
                    { 195, null, "English", "Skeletor", "White", 1 },
                    { 196, null, "English", "Chivalry", "White", 1 },
                    { 197, null, "English", "Sharing needles", "White", 1 },
                    { 198, null, "English", "Being rich", "White", 1 },
                    { 199, null, "English", "Muzzy", "White", 1 },
                    { 200, null, "English", "Count Chocula", "White", 1 },
                    { 201, null, "English", "Spontaneous human combustion", "White", 1 },
                    { 202, null, "English", "College", "White", 1 },
                    { 203, null, "English", "Necrophilia", "White", 1 },
                    { 204, null, "English", "The Chinese gymnastics team", "White", 1 },
                    { 205, null, "English", "Global warming", "White", 1 },
                    { 206, null, "English", "Farting and walking away", "White", 1 },
                    { 207, null, "English", "Emotions", "White", 1 },
                    { 208, null, "English", "Uppercuts", "White", 1 },
                    { 209, null, "English", "Cookie Monster devouring the Eucharist wafers", "White", 1 },
                    { 210, null, "English", "Stifling a iggle at the mention of Hutus and Tutsis", "White", 1 },
                    { 211, null, "English", "Penis envy", "White", 1 },
                    { 212, null, "English", "Letting yourself go", "White", 1 },
                    { 213, null, "English", "White people", "White", 1 },
                    { 214, null, "English", "Dick Cheney", "White", 1 },
                    { 215, null, "English", "Leaving an awkward voicemail", "White", 1 },
                    { 216, null, "English", "Yeast", "White", 1 },
                    { 217, null, "English", "Natural selection", "White", 1 },
                    { 218, null, "English", "Masturbation", "White", 1 },
                    { 219, null, "English", "Twinkies (r)", "White", 1 },
                    { 220, null, "English", "A LAN Party", "White", 1 },
                    { 221, null, "English", "Opposable thumbs", "White", 1 },
                    { 222, null, "English", "A grande sugar-free iced soy caramel macchiato", "White", 1 },
                    { 223, null, "English", "Soiling oneself", "White", 1 },
                    { 224, null, "English", "A sassy black woman", "White", 1 },
                    { 225, null, "English", "Sperm whales", "White", 1 },
                    { 226, null, "English", "Teaching a robot to love", "White", 1 },
                    { 227, null, "English", "Scrubbing under the folds", "White", 1 },
                    { 228, null, "English", "A drive-by shooting", "White", 1 },
                    { 229, null, "English", "Whipping it out", "White", 1 },
                    { 230, null, "English", "Panda sex", "White", 1 },
                    { 231, null, "English", "Catapults", "White", 1 },
                    { 232, null, "English", "Will Smith", "White", 1 },
                    { 233, null, "English", "Toni Morrison's vagina", "White", 1 },
                    { 234, null, "English", "Five-Dollar Foot-longs (tm)", "White", 1 },
                    { 235, null, "English", "Land minds", "White", 1 },
                    { 236, null, "English", "A sea of troubles", "White", 1 },
                    { 237, null, "English", "A zesty breakfast burrito", "White", 1 },
                    { 238, null, "English", "Christopher Walken", "White", 1 },
                    { 239, null, "English", "Friction", "White", 1 },
                    { 240, null, "English", "Balls", "White", 1 },
                    { 241, null, "English", "AIDS", "White", 1 },
                    { 242, null, "English", "The KKK", "White", 1 },
                    { 243, null, "English", "Figgy pudding", "White", 1 },
                    { 244, null, "English", "Seppuku", "White", 1 },
                    { 245, null, "English", "Marky Mark and the Funky Bunch", "White", 1 },
                    { 246, null, "English", "Gandhi", "White", 1 },
                    { 247, null, "English", "Dave Matthews Band", "White", 1 },
                    { 248, null, "English", "Preteens", "White", 1 },
                    { 249, null, "English", "The token minority", "White", 1 },
                    { 250, null, "English", "Friends with benefits", "White", 1 },
                    { 251, null, "English", "Re-gifting", "White", 1 },
                    { 252, null, "English", "Pixelated bukkake", "White", 1 },
                    { 253, null, "English", "Substitute teachers", "White", 1 },
                    { 254, null, "English", "Take-backsies", "White", 1 },
                    { 255, null, "English", "A thermonuclear detonation", "White", 1 },
                    { 256, null, "English", "The Tempur-Pedic (r) Swedish Sleep System (tm)", "White", 1 },
                    { 257, null, "English", "Waiting 'til marriage", "White", 1 },
                    { 258, null, "English", "A tiny horse", "White", 1 },
                    { 259, null, "English", "A can of whoop-ass", "White", 1 },
                    { 260, null, "English", "Dental dams", "White", 1 },
                    { 261, null, "English", "Feeding Rosie O'Donnell", "White", 1 },
                    { 262, null, "English", "Old-people smell", "White", 1 },
                    { 263, null, "English", "Genghis Khan", "White", 1 },
                    { 264, null, "English", "Authentic Mexican cuisine", "White", 1 },
                    { 265, null, "English", "Oversized lollipops", "White", 1 },
                    { 266, null, "English", "Garth Brooks", "White", 1 },
                    { 267, null, "English", "Keanu Reeves", "White", 1 },
                    { 268, null, "English", "Drinking alone", "White", 1 },
                    { 269, null, "English", "The American Dream", "White", 1 },
                    { 270, null, "English", "Taking off your shirt", "White", 1 },
                    { 271, null, "English", "Giving 110%", "White", 1 },
                    { 272, null, "English", "Flesh-eating bacteria", "White", 1 },
                    { 273, null, "English", "Child abuse", "White", 1 },
                    { 274, null, "English", "A cooler full of organs", "White", 1 },
                    { 275, null, "English", "A moment of silence", "White", 1 },
                    { 276, null, "English", "The Rapture", "White", 1 },
                    { 277, null, "English", "Keeping Christ in Christmas", "White", 1 },
                    { 278, null, "English", "RoboCop", "White", 1 },
                    { 279, null, "English", "That one gay Teletubby", "White", 1 },
                    { 280, null, "English", "Sweet, sweet vengeance", "White", 1 },
                    { 281, null, "English", "Fancy Feast (r)", "White", 1 },
                    { 282, null, "English", "Pooping back and forth. Forever.", "White", 1 },
                    { 283, null, "English", "Being a motherfucking sorcerer", "White", 1 },
                    { 284, null, "English", "Jewish fraternities", "White", 1 },
                    { 285, null, "English", "Edible underpants", "White", 1 },
                    { 286, null, "English", "Poor people", "White", 1 },
                    { 287, null, "English", "All-you-can-eat shrimp for $4.99", "White", 1 },
                    { 288, null, "English", "Britney Spears at 55", "White", 1 },
                    { 289, null, "English", "That thing that electrocutes your abs", "White", 1 },
                    { 290, null, "English", "The folly of man", "White", 1 },
                    { 291, null, "English", "Fiery poops", "White", 1 },
                    { 292, null, "English", "Cards Against Humanity", "White", 1 },
                    { 293, null, "English", "A murder most foul", "White", 1 },
                    { 294, null, "English", "Me time", "White", 1 },
                    { 295, null, "English", "The inevitable heat death of the universe", "White", 1 },
                    { 296, null, "English", "Nocturnal emissions", "White", 1 },
                    { 297, null, "English", "Daddy issues", "White", 1 },
                    { 298, null, "English", "The hardworking Mexican", "White", 1 },
                    { 299, null, "English", "Natalie Portman", "White", 1 },
                    { 300, null, "English", "Waking up half-naked in a Denny's parking lot", "White", 1 },
                    { 301, null, "English", "Nipple blades", "White", 1 },
                    { 302, null, "English", "Assless chaps", "White", 1 },
                    { 303, null, "English", "Full frontal nudity", "White", 1 },
                    { 304, null, "English", "Hulk Hogan", "White", 1 },
                    { 305, null, "English", "Passive-aggression", "White", 1 },
                    { 306, null, "English", "Ronald Reagan", "White", 1 },
                    { 307, null, "English", "Vehicular manslaughter", "White", 1 },
                    { 308, null, "English", "Menstruation", "White", 1 },
                    { 309, null, "English", "Pulling out", "White", 1 },
                    { 310, null, "English", "Picking up girls at the abortion clinic", "White", 1 },
                    { 311, null, "English", "The homosexual agenda", "White", 1 },
                    { 312, null, "English", "The Holy Bible", "White", 1 },
                    { 313, null, "English", "World peace", "White", 1 },
                    { 314, null, "English", "Dropping a chandelier on your enemies and riding the rope up", "White", 1 },
                    { 315, null, "English", "Testicular torsion", "White", 1 },
                    { 316, null, "English", "The milk man", "White", 1 },
                    { 317, null, "English", "A time-travel paradox", "White", 1 },
                    { 318, null, "English", "Hot Pockets (r)", "White", 1 },
                    { 319, null, "English", "Guys who don't call", "White", 1 },
                    { 320, null, "English", "Eating the last known bison", "White", 1 },
                    { 321, null, "English", "Darth Vader", "White", 1 },
                    { 322, null, "English", "Scalping", "White", 1 },
                    { 323, null, "English", "Homeless people", "White", 1 },
                    { 324, null, "English", "The World of Warcraft", "White", 1 },
                    { 325, null, "English", "Gloryholes", "White", 1 },
                    { 326, null, "English", "Saxophone solos", "White", 1 },
                    { 327, null, "English", "Sean Connery", "White", 1 },
                    { 328, null, "English", "God", "White", 1 },
                    { 329, null, "English", "Intelligent design", "White", 1 },
                    { 330, null, "English", "The taint; the grundle; the fleshy fun-bridge", "White", 1 },
                    { 331, null, "English", "Friendly fire", "White", 1 },
                    { 332, null, "English", "Keg stands", "White", 1 },
                    { 333, null, "English", "Eugenics", "White", 1 },
                    { 334, null, "English", "A good sniff", "White", 1 },
                    { 335, null, "English", "Lockjaw", "White", 1 },
                    { 336, null, "English", "A neglected Tamagotchi (tm)", "White", 1 },
                    { 337, null, "English", "The People's Elbow", "White", 1 },
                    { 338, null, "English", "Robert Downey, Jr.", "White", 1 },
                    { 339, null, "English", "The heart of a child", "White", 1 },
                    { 340, null, "English", "Seduction", "White", 1 },
                    { 341, null, "English", "Smallpox blankets", "White", 1 },
                    { 342, null, "English", "Licking things to claim them as your own", "White", 1 },
                    { 343, null, "English", "A salty surprise", "White", 1 },
                    { 344, null, "English", "Poorly-timed Holocaust jokes", "White", 1 },
                    { 345, null, "English", "My soul", "White", 1 },
                    { 346, null, "English", "My sex life", "White", 1 },
                    { 347, null, "English", "Pterodactyl eggs", "White", 1 },
                    { 348, null, "English", "Altar boys", "White", 1 },
                    { 349, null, "English", "Forgetting the Alamo", "White", 1 },
                    { 350, null, "English", "72 virgins", "White", 1 },
                    { 351, null, "English", "Raping and pillaging", "White", 1 },
                    { 352, null, "English", "Pedophiles", "White", 1 },
                    { 353, null, "English", "Eastern European Turbo-folk music", "White", 1 },
                    { 354, null, "English", "A snapping turtle biting the tip of your penis", "White", 1 },
                    { 355, null, "English", "Pabst Blue Ribbon", "White", 1 },
                    { 356, null, "English", "Domino's (tm) Oreo (tm) Dessert Pizza", "White", 1 },
                    { 357, null, "English", "My collection of high-tech sex toys", "White", 1 },
                    { 358, null, "English", "A middle-aged man on roller skates", "White", 1 },
                    { 359, null, "English", "The Blood of Christ", "White", 1 },
                    { 360, null, "English", "Half-assed foreplay", "White", 1 },
                    { 361, null, "English", "Free samples", "White", 1 },
                    { 362, null, "English", "Douchebags on their iPhones", "White", 1 },
                    { 363, null, "English", "Hurricane Katrina", "White", 1 },
                    { 364, null, "English", "Wearing underwear inside-out to avoid doing laundry", "White", 1 },
                    { 365, null, "English", "Republicans", "White", 1 },
                    { 366, null, "English", "The glass ceiling", "White", 1 },
                    { 367, null, "English", "A foul mouth", "White", 1 },
                    { 368, null, "English", "Jerking off into a pool of children's tears", "White", 1 },
                    { 369, null, "English", "Getting really high", "White", 1 },
                    { 370, null, "English", "The deformed", "White", 1 },
                    { 371, null, "English", "Michelle Obama's arms", "White", 1 },
                    { 372, null, "English", "Explosions", "White", 1 },
                    { 373, null, "English", "The Übermensch", "White", 1 },
                    { 374, null, "English", "Donald Trump", "White", 1 },
                    { 375, null, "English", "Sarah Palin", "White", 1 },
                    { 376, null, "English", "Attitude", "White", 1 },
                    { 377, null, "English", "This answer is postmodern", "White", 1 },
                    { 378, null, "English", "Crumpets with the Queen", "White", 1 },
                    { 379, null, "English", "Frolicking", "White", 1 },
                    { 380, null, "English", "Team-building exercises", "White", 1 },
                    { 381, null, "English", "Repression", "White", 1 },
                    { 382, null, "English", "Road head", "White", 1 },
                    { 383, null, "English", "A bag of magic beans", "White", 1 },
                    { 384, null, "English", "An asymmetric boob job", "White", 1 },
                    { 385, null, "English", "Dead parents", "White", 1 },
                    { 386, null, "English", "Public ridicule", "White", 1 },
                    { 387, null, "English", "A mating display", "White", 1 },
                    { 388, null, "English", "A mime having a stroke", "White", 1 },
                    { 389, null, "English", "Stephen Hawking talking dirty", "White", 1 },
                    { 390, null, "English", "African children", "White", 1 },
                    { 391, null, "English", "Mouth herpes", "White", 1 },
                    { 392, null, "English", "Overcompensation", "White", 1 },
                    { 393, null, "English", "Bill Nye the Science Guy", "White", 1 },
                    { 394, null, "English", "Bitches", "White", 1 },
                    { 395, null, "English", "Italians", "White", 1 },
                    { 396, null, "English", "Have some more kugel", "White", 1 },
                    { 397, null, "English", "A windmill full of corpses", "White", 1 },
                    { 398, null, "English", "Her Royal Highness, Queen Elizabeth II", "White", 1 },
                    { 399, null, "English", "Crippling debt", "White", 1 },
                    { 400, null, "English", "Adderall (tm)", "White", 1 },
                    { 401, null, "English", "A stray pube", "White", 1 },
                    { 402, null, "English", "Shorties and blunts", "White", 1 },
                    { 403, null, "English", "Passing a kidney stone", "White", 1 },
                    { 404, null, "English", "Prancing", "White", 1 },
                    { 405, null, "English", "Leprosy", "White", 1 },
                    { 406, null, "English", "A brain tumor", "White", 1 },
                    { 407, null, "English", "Bees?", "White", 1 },
                    { 408, null, "English", "Puppies!", "White", 1 },
                    { 409, null, "English", "Cockfights", "White", 1 },
                    { 410, null, "English", "Kim Jong-Il", "White", 1 },
                    { 411, null, "English", "Hope", "White", 1 },
                    { 412, null, "English", "8 oz. of sweet Mexican black-tar heroin", "White", 1 },
                    { 413, null, "English", "Incest", "White", 1 },
                    { 414, null, "English", "Grave robbing", "White", 1 },
                    { 415, null, "English", "Asians who aren't good at math", "White", 1 },
                    { 416, null, "English", "Alcoholism", "White", 1 },
                    { 417, null, "English", "(I am doing Kegels right now.)", "White", 1 },
                    { 418, null, "English", "Justin Bieber", "White", 1 },
                    { 419, null, "English", "The Jews", "White", 1 },
                    { 420, null, "English", "Bestiality", "White", 1 },
                    { 421, null, "English", "Winking at old people", "White", 1 },
                    { 422, null, "English", "Drum circles", "White", 1 },
                    { 423, null, "English", "Kids with ass cancer", "White", 1 },
                    { 424, null, "English", "Loose lips", "White", 1 },
                    { 425, null, "English", "Auschwitz", "White", 1 },
                    { 426, null, "English", "Civilian casualties", "White", 1 },
                    { 427, null, "English", "Inappropriate yodeling", "White", 1 },
                    { 428, null, "English", "Tangled Slinkys", "White", 1 },
                    { 429, null, "English", "Being on fire", "White", 1 },
                    { 430, null, "English", "The Thong Song", "White", 1 },
                    { 431, null, "English", "A vajazzled vagina", "White", 1 },
                    { 432, null, "English", "Riding off into the sunset", "White", 1 },
                    { 433, null, "English", "Exchanging pleasantries", "White", 1 },
                    { 434, null, "English", "My relationship status", "White", 1 },
                    { 435, null, "English", "Shaquille O'Neals's acting career", "White", 1 },
                    { 436, null, "English", "Being fabulous", "White", 1 },
                    { 437, null, "English", "Lactation", "White", 1 },
                    { 438, null, "English", "Not reciprocating oral sex", "White", 1 },
                    { 439, null, "English", "Sobbing into a Hungry-Man (r) Frozen Dinner", "White", 1 },
                    { 440, null, "English", "My genitals", "White", 1 },
                    { 441, null, "English", "Date rape", "White", 1 },
                    { 442, null, "English", "Ring Pops (tm)", "White", 1 },
                    { 443, null, "English", "GoGurt", "White", 1 },
                    { 444, null, "English", "Judge Judy", "White", 1 },
                    { 445, null, "English", "Lumberjack fantasies", "White", 1 },
                    { 446, null, "English", "The gays", "White", 1 },
                    { 447, null, "English", "Scientology", "White", 1 },
                    { 448, null, "English", "Estrogen", "White", 1 },
                    { 449, null, "English", "Police brutality", "White", 1 },
                    { 450, null, "English", "Passable transvestites", "White", 1 },
                    { 451, null, "English", "The Virginia Tech Massacre", "White", 1 },
                    { 452, null, "English", "Tiger Woods", "White", 1 },
                    { 453, null, "English", "Dick fingers", "White", 1 },
                    { 454, null, "English", "Racism", "White", 1 },
                    { 455, null, "English", "Glenn Beck being harried by a swarm of buzzards", "White", 1 },
                    { 456, null, "English", "Surprise sex!", "White", 1 },
                    { 457, null, "English", "Classist undertones", "White", 1 },
                    { 458, null, "English", "Booby-trapping the house to foil burglars", "White", 1 },
                    { 459, null, "English", "New Age music", "White", 1 },
                    { 460, null, "English", "PCP", "White", 1 },
                    { 461, null, "English", "A lifetime of sadness", "White", 1 },
                    { 462, null, "English", "Doin' it in the butt", "White", 1 },
                    { 463, null, "English", "Swooping", "White", 1 },
                    { 464, null, "English", "The Hamburglar", "White", 1 },
                    { 465, null, "English", "Tentacle porn", "White", 1 },
                    { 466, null, "English", "A hot mess", "White", 1 },
                    { 467, null, "English", "Too much hair gel", "White", 1 },
                    { 468, null, "English", "A look-see", "White", 1 },
                    { 469, null, "English", "Not giving a shit about the Third World", "White", 1 },
                    { 470, null, "English", "American Gladiators", "White", 1 },
                    { 471, null, "English", "The Kool-Aid man", "White", 1 },
                    { 472, null, "English", "Mr Snuffleupagus", "White", 1 },
                    { 473, null, "English", "Barack Obama", "White", 1 },
                    { 474, null, "English", "Golden showers", "White", 1 },
                    { 475, null, "English", "Wiping her butt", "White", 1 },
                    { 476, null, "English", "Queefing", "White", 1 },
                    { 477, null, "English", "Getting drunk on mouthwash", "White", 1 },
                    { 478, null, "English", "An M. Night Shyamalan plot twist", "White", 1 },
                    { 479, null, "English", "A robust mongoloid", "White", 1 },
                    { 480, null, "English", "Nazis", "White", 1 },
                    { 481, null, "English", "White privilege", "White", 1 },
                    { 482, null, "English", "An erection that lasts longer than four hours", "White", 1 },
                    { 483, null, "English", "A disappointing birthday party", "White", 1 },
                    { 484, null, "English", "Puberty", "White", 1 },
                    { 485, null, "English", "Two midgets shitting in a bucket", "White", 1 },
                    { 486, null, "English", "Wifely duties", "White", 1 },
                    { 487, null, "English", "The forbidden fruit", "White", 1 },
                    { 488, null, "English", "Getting so angry that you pop a boner", "White", 1 },
                    { 489, null, "English", "Sexual tension", "White", 1 },
                    { 490, null, "English", "Third base", "White", 1 },
                    { 491, null, "English", "A gassy antelope", "White", 1 },
                    { 492, null, "English", "Those times when you get sand in your vagina", "White", 1 },
                    { 493, null, "English", "A Super Soaker (tm) full of cat pee", "White", 1 },
                    { 494, null, "English", "Muhammad (Praise Be Unto Him)", "White", 1 },
                    { 495, null, "English", "Racially-biased SAT questions", "White", 1 },
                    { 496, null, "English", "Porn stars", "White", 1 },
                    { 497, null, "English", "A fetus", "White", 1 },
                    { 498, null, "English", "Obesity", "White", 1 },
                    { 499, null, "English", "When you fart and a little bit comes out", "White", 1 },
                    { 500, null, "English", "Oompa-Loompas", "White", 1 },
                    { 501, null, "English", "BATMAN!!!", "White", 1 },
                    { 502, null, "English", "Black people", "White", 1 },
                    { 503, null, "English", "Tasteful sideboob", "White", 1 },
                    { 504, null, "English", "Hot people", "White", 1 },
                    { 505, null, "English", "Grandma", "White", 1 },
                    { 506, null, "English", "Copping a feel", "White", 1 },
                    { 507, null, "English", "The Trail of Tears", "White", 1 },
                    { 508, null, "English", "Famine", "White", 1 },
                    { 509, null, "English", "Finger painting", "White", 1 },
                    { 510, null, "English", "The miracle of childbirth", "White", 1 },
                    { 511, null, "English", "Goats eating cans", "White", 1 },
                    { 512, null, "English", "A monkey smoking a cigar", "White", 1 },
                    { 513, null, "English", "Faith healing", "White", 1 },
                    { 514, null, "English", "Parting the Red Sea", "White", 1 },
                    { 515, null, "English", "Dead babies", "White", 1 },
                    { 516, null, "English", "The Amish", "White", 1 },
                    { 517, null, "English", "Impotence", "White", 1 },
                    { 518, null, "English", "Child beauty pageants", "White", 1 },
                    { 519, null, "English", "Centaurs", "White", 1 },
                    { 520, null, "English", "AXE Body Spray", "White", 1 },
                    { 521, null, "English", "Kanye West", "White", 1 },
                    { 522, null, "English", "Women's suffrage", "White", 1 },
                    { 523, null, "English", "Children on leashes", "White", 1 },
                    { 524, null, "English", "Harry Potter erotica", "White", 1 },
                    { 525, null, "English", "The Dance of the Sugar Plum Fairy", "White", 1 },
                    { 526, null, "English", "Lance Armstrong's missing testicle", "White", 1 },
                    { 527, null, "English", "Dwarf tossing", "White", 1 },
                    { 528, null, "English", "Mathletes", "White", 1 },
                    { 529, null, "English", "Lunchables (tm)", "White", 1 },
                    { 530, null, "English", "Women in yogurt commercials", "White", 1 },
                    { 531, null, "English", "John Wilkes Booth", "White", 1 },
                    { 532, null, "English", "Powerful thighs", "White", 1 },
                    { 533, null, "English", "Mr. Clean, right behind you", "White", 1 },
                    { 534, null, "English", "Multiple stab wounds", "White", 1 },
                    { 535, null, "English", "Cybernetic enhancements", "White", 1 },
                    { 536, null, "English", "Serfdom", "White", 1 },
                    { 537, null, "English", "Another god-damn vampire movie", "White", 1 },
                    { 538, null, "English", "Glenn Beck catching his scrotum on a curtain hook", "White", 1 },
                    { 539, null, "English", "A big hoopla about nothing", "White", 1 },
                    { 540, null, "English", "Peeing a little bit", "White", 1 },
                    { 541, null, "English", "The Hustle", "White", 1 },
                    { 542, null, "English", "Ghosts", "White", 1 },
                    { 543, null, "English", "Bananas in Pajamas", "White", 1 },
                    { 544, null, "English", "Active listening", "White", 1 },
                    { 545, null, "English", "Dry heaving", "White", 1 },
                    { 546, null, "English", "Kamikaze pilots", "White", 1 },
                    { 547, null, "English", "The Force", "White", 1 },
                    { 548, null, "English", "Anal beads", "White", 1 },
                    { 549, null, "English", "The Make-A-Wish (r) Foundation", "White", 1 }
                });

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "Id", "Language", "Name", "SafeContent", "UserId", "black", "white" },
                values: new object[] { 1, "English", "Core Game", false, 1, (short)89, (short)460 });

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
