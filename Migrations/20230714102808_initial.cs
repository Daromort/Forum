using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Forum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Karma = table.Column<int>(type: "int", nullable: false),
                    TwitterProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChatUsers",
                columns: table => new
                {
                    ChatID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUsers", x => new { x.ChatID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ChatUsers_Chats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "Chats",
                        principalColumn: "ChatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    memberCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Users_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "Chats",
                        principalColumn: "ChatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupsUsers",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsUsers", x => new { x.GroupID, x.UserID });
                    table.ForeignKey(
                        name: "FK_GroupsUsers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    LikesCount = table.Column<int>(type: "int", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(280)", maxLength: 280, nullable: false),
                    LikesCount = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Comments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PostsDislikes",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsDislikes", x => new { x.PostID, x.UserID });
                    table.ForeignKey(
                        name: "FK_PostsDislikes_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsDislikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsLikes",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsLikes", x => new { x.PostID, x.UserID });
                    table.ForeignKey(
                        name: "FK_PostsLikes_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsLikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostsTags",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsTags", x => new { x.PostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_PostsTags_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsTags_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentsDislikes",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CommentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsDislikes", x => new { x.UserID, x.CommentID });
                    table.ForeignKey(
                        name: "FK_CommentsDislikes_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentsDislikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentsLikes",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    CommentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsLikes", x => new { x.UserID, x.CommentID });
                    table.ForeignKey(
                        name: "FK_CommentsLikes_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentsLikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "outdoor" },
                    { 2, "fresh" },
                    { 3, "downtown" },
                    { 4, "football" },
                    { 5, "favorite" },
                    { 6, "food" },
                    { 7, "market" },
                    { 8, "bar" },
                    { 9, "beach" },
                    { 10, "suburbs" },
                    { 11, "player" },
                    { 12, "team" },
                    { 13, "bio" },
                    { 14, "around" },
                    { 15, "drinks" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Description", "Email", "FacebookProfile", "FirstName", "InstagramProfile", "IsBlocked", "Karma", "LastName", "Password", "PhoneNumber", "Photo", "RegisteredAt", "Role", "TwitterProfile", "Username" },
                values: new object[,]
                {
                    { 1, "I am a user with a simple description.", "JohnDow@gmail.com", null, "John", null, false, 0, "Dow", "JohnyDow@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(122), "User", null, "TheCrazyJohn" },
                    { 2, "I am a user with a simple description.", "PeterPan@gmail.com", null, "Peter", null, false, 0, "Pan", "PeterPan@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(129), "User", null, "TheCrazyPete" },
                    { 3, "I am a user with a simple description.", "ScarletDavidson@gmail.com", null, "Scarlet", null, false, 0, "Davidson", "ScarletDavidson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(133), "User", null, "TheCrazyScarley" },
                    { 4, "I am a user with a simple description.", "MelonyClark@gmail.com", null, "Melony", null, false, 0, "Clark", "MelonyClark@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(191), "User", null, "TheCrazyClark" },
                    { 5, "I am an admin with a simple description.", "ChrisRhea@gmail.com", null, "Chris", null, false, 0, "Rhea", "ChrisRhea", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(194), "Admin", null, "TheCrazyChris" },
                    { 6, "I am a user with a simple description.", "alice.smith@example.com", null, "Alice", null, false, 0, "Smith", "AliceSmith@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(201), "User", null, "alice123" },
                    { 7, "I am a user with a simple description.", "bob.johnson@example.com", null, "Bob", null, false, 0, "Johnson", "BobJohnson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(205), "User", null, "bob123" },
                    { 8, "I am a user with a simple description.", "emma.davis@example.com", null, "Emma", null, false, 0, "Davis", "EmmaDavis@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(208), "User", null, "emma123" },
                    { 9, "I am a user with a simple description.", "michael.wilson@example.com", null, "Michael", null, false, 0, "Wilson", "MichaelWilson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(211), "User", null, "michael123" },
                    { 10, "I am a user with a simple description.", "sophia.thomas31231312@example.com", null, "Sophia", null, false, 0, "Thomas", "SophiaThomas@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(217), "User", null, "sophia123" },
                    { 11, "I am a user with a simple description.", "jacob.brown@example.com", null, "Jacob", null, false, 0, "Brown", "JacobBrown@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(220), "User", null, "jacob123" },
                    { 12, "I am a user with a simple description.", "olivia.taylor@example.com", null, "Olivia", null, false, 0, "Taylor", "OliviaTaylor@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(224), "User", null, "olivia123" },
                    { 13, "I am a user with a simple description.", "ethan.anderson@example.com", null, "Ethan", null, false, 0, "Anderson", "EthanAnderson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(227), "User", null, "ethan123" },
                    { 14, "I am a user with a simple description.", "oliver.wilson@example.com", null, "Oliver", null, false, 0, "Wilson", "OliverWilson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(268), "User", null, "oliver123" },
                    { 15, "I am a user with a simple description.", "amelia.brown@example.com", null, "Amelia", null, false, 0, "Brown", "AmeliaBrown@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(272), "User", null, "amelia123" },
                    { 16, "I am a user with a simple description.", "liam.taylor@example.com", null, "Liam", null, false, 0, "Taylor", "LiamTaylor@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(275), "User", null, "liam123" },
                    { 17, "I am a user with a simple description.", "sophia.thomas@example.com", null, "Sophia", null, false, 0, "Thomas", "SophiaThomas@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(279), "User", null, "sophia123" },
                    { 18, "I am a user with a simple description.", "noah.clark@example.com", null, "Noah", null, false, 0, "Clark", "NoahClark@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(283), "User", null, "noah123" },
                    { 19, "I am a user with a simple description.", "ava.anderson@example.com", null, "Ava", null, false, 0, "Anderson", "AvaAnderson@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(286), "User", null, "ava123" },
                    { 20, "I am a user with a simple description.", "mason.miller@example.com", null, "Mason", null, false, 0, "Miller", "MasonMiller@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(289), "User", null, "mason123" },
                    { 21, "I am a user with a simple description.", "isabella.martin@example.com", null, "Isabella", null, false, 0, "Martin", "IsabellaMartin@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(292), "User", null, "isabella123" },
                    { 22, "I am a user with a simple description.", "james.walker@example.com", null, "James", null, false, 0, "Walker", "JamesWalker@", null, null, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(295), "User", null, "james123" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "ID", "CreatedAt", "CreatorID", "Description", "Name", "memberCount" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(940), 1, "A group for passionate travelers.", "Travel Enthusiasts", 1 },
                    { 2, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(945), 2, "For those who appreciate good food.", "Food Lovers", 1 },
                    { 3, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(947), 3, "Get fit and healthy together.", "Fitness Fanatics", 1 },
                    { 4, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(949), 6, "Discuss your favorite books and authors.", "Book Club", 1 },
                    { 5, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(951), 6, "Share your thoughts on the latest movies.", "Movie Buffs", 1 },
                    { 6, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(954), 6, "Explore the latest in technology.", "Tech Geeks", 1 },
                    { 7, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(956), 6, "For those who love sports and competition.", "Sports Fans", 1 },
                    { 8, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(958), 8, "Appreciate various forms of art and culture.", "Art and Culture", 1 },
                    { 9, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(959), 9, "Capture and share stunning photographs.", "Photography Enthusiasts", 1 },
                    { 10, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(962), 10, "Discover the beauty of the natural world.", "Nature Explorers", 1 },
                    { 11, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(964), 9, "Stay updated with the latest fashion trends.", "Fashion and Style", 1 },
                    { 12, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(1003), 12, "Discuss your favorite genres and artists.", "Music Lovers", 1 },
                    { 13, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(1006), 13, "Connect with fellow gamers.", "Gaming Community", 1 },
                    { 14, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(1008), 13, "Share recipes and cooking tips.", "Cooking Enthusiasts", 1 },
                    { 15, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(1010), 13, "Embark on thrilling outdoor activities.", "Outdoor Adventures", 1 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "ID", "Content", "CreatedAt", "GroupID", "IsBlocked", "LikesCount", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, "Let's discuss the suburbs, downtown and Venice beach.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(435), null, false, 0, "Hollywood is the best place to live.", 1 },
                    { 2, "Where in VA I can find a fresh food market?", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(440), null, false, 0, "Bio fresh food market on Sundays in Virginia?", 2 },
                    { 3, "Guys, please share your experience and let me know which is your favorite place to go out.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(443), null, false, 0, "Best bars in the downtown DC?", 3 },
                    { 4, "Who are your favorite players teams and etc.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(444), null, false, 0, "Football is my life.", 4 },
                    { 5, "Share your best travel tips and recommendations for visiting Europe.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(446), null, false, 0, "Traveling Tips for Europe", 5 },
                    { 6, "Discuss the latest movies hitting the theaters and share your reviews.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(450), null, false, 0, "New Movie Releases", 6 },
                    { 7, "Let's exchange our favorite recipes and cooking techniques.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(452), null, false, 0, "Favorite Recipes", 7 },
                    { 8, "Looking for some new books to read. Any recommendations?", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(461), null, false, 0, "Book Recommendations", 8 },
                    { 9, "Share your fitness journey, tips, and motivational stories.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(463), null, false, 0, "Fitness Tips and Motivation", 9 },
                    { 10, "Discuss the latest trends and advancements in the world of technology.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(467), null, false, 0, "Technology Trends", 10 },
                    { 11, "What are you currently listening to? Share your favorite music.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(468), null, false, 0, "Music Recommendations", 11 },
                    { 12, "Share your outdoor adventures and recommend the best hiking trails.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(470), null, false, 0, "Outdoor Adventures", 12 },
                    { 13, "Discuss home improvement projects and share your before/after pictures.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(472), null, false, 0, "Home Improvement Ideas", 13 },
                    { 14, "Join the discussion about the latest games and gaming news.", new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(474), null, false, 0, "Gaming Community", 14 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "ID", "CreatedAt", "IsBlocked", "LikesCount", "Message", "ParentID", "PostID", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(750), false, 0, "I don't like Hollywood at all, there are many homeless people there.", null, 1, 4 },
                    { 2, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(754), false, 0, "I fucking love Venice and Santa Monica. The sunsets there are fucking awesome.", null, 1, 3 },
                    { 3, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(756), false, 0, "Downtown sucks, however I was enjoyed Venice as well.", null, 1, 1 },
                    { 4, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(757), false, 0, "There is one in Springfield near the 7 eleven.", null, 2, 1 },
                    { 5, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(795), false, 0, "Bro, try the one in Alexandria. Food there is always fresh and nice.", null, 2, 4 },
                    { 6, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(798), false, 0, "You  should visit the Off the record bar.", null, 3, 1 },
                    { 7, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(800), false, 0, "Any bar in the downtown is pretty fine to me.", null, 3, 2 },
                    { 8, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(801), false, 0, "Venice Beach is indeed beautiful. I love the vibrant atmosphere.", null, 1, 6 },
                    { 9, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(803), false, 0, "Downtown has its charm, but I agree that Venice is more enjoyable.", null, 1, 7 },
                    { 10, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(806), false, 0, "I've heard great things about the fresh food market in Arlington.", null, 2, 8 },
                    { 11, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(808), false, 0, "Alexandria also has a nice farmers' market. Check it out!", null, 2, 9 },
                    { 12, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(810), false, 0, "The Pub is my favorite bar in downtown DC. Great drinks and atmosphere.", null, 3, 10 },
                    { 13, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(813), false, 0, "I enjoy trying different bars in the area. There are so many options!", null, 3, 11 },
                    { 14, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(815), false, 0, "Football is an amazing sport. I'm a huge fan of the NFL.", null, 4, 12 },
                    { 15, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(817), false, 0, "My favorite team is the New England Patriots. They always deliver.", null, 4, 13 },
                    { 16, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(819), false, 0, "Europe is a dream destination for many travelers. Can't wait to explore it!", null, 5, 14 },
                    { 17, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(821), false, 0, "Don't forget to visit Paris and Rome. They have amazing landmarks.", null, 5, 1 },
                    { 18, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(823), false, 0, "The new Avengers movie is a must-watch. It's action-packed!", null, 6, 2 },
                    { 19, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(825), false, 0, "I'm more into indie films. Any recommendations for those?", null, 6, 3 },
                    { 20, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(827), false, 0, "The rooftop bar at W Hotel has stunning views of the city.", null, 7, 4 },
                    { 21, new DateTime(2023, 7, 14, 10, 28, 8, 343, DateTimeKind.Utc).AddTicks(829), false, 0, "I enjoy the lively atmosphere at the Adams Morgan bars.", null, 7, 5 }
                });

            migrationBuilder.InsertData(
                table: "PostsTags",
                columns: new[] { "PostID", "TagID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 9 },
                    { 1, 10 },
                    { 2, 2 },
                    { 2, 6 },
                    { 2, 7 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserID",
                table: "ChatUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentID",
                table: "Comments",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsDislikes_CommentID",
                table: "CommentsDislikes",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsLikes_CommentID",
                table: "CommentsLikes",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorID",
                table: "Groups",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsUsers_UserID",
                table: "GroupsUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatID",
                table: "Messages",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserID",
                table: "Messages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GroupID",
                table: "Posts",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserID",
                table: "Posts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostsDislikes_UserID",
                table: "PostsDislikes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostsLikes_UserID",
                table: "PostsLikes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostsTags_TagID",
                table: "PostsTags",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUsers");

            migrationBuilder.DropTable(
                name: "CommentsDislikes");

            migrationBuilder.DropTable(
                name: "CommentsLikes");

            migrationBuilder.DropTable(
                name: "GroupsUsers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PostsDislikes");

            migrationBuilder.DropTable(
                name: "PostsLikes");

            migrationBuilder.DropTable(
                name: "PostsTags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
