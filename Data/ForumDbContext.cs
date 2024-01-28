using System.IO;
using System.Runtime.CompilerServices;
using Forum_Management_System.Data.EntityConfigurations;
using Forum_Management_System.Models;
using Forum_Management_System.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json.Linq;


namespace Forum_Management_System.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostsLikes { get; set; }
        public DbSet<PostDislike> PostsDislikes { get; set; }
        public DbSet<CommentLike> CommentsLikes { get; set; }
        public DbSet<CommentDislike> CommentsDislikes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostsTags { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUser> ChatsUsers { get; set; }
        public DbSet<Group > Groups { get; set; }
        public DbSet<GroupUser> GroupsUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentLikeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentDislikeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostLikeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostDislikeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostTagEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupUserEntityTypeConfiguration());

            List<User> users = new List<User>
            {
                new User { ID = 1, FirstName = "John", LastName = "Dow", Email = "JohnDow@gmail.com", Username = "TheCrazyJohn", Password = "JohnyDow@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 2, FirstName = "Peter", LastName = "Pan", Email = "PeterPan@gmail.com", Username = "TheCrazyPete", Password = "PeterPan@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 3, FirstName = "Scarlet", LastName = "Davidson", Email = "ScarletDavidson@gmail.com", Username = "TheCrazyScarley", Password = "ScarletDavidson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 4, FirstName = "Melony", LastName = "Clark", Email = "MelonyClark@gmail.com", Username = "TheCrazyClark", Password = "MelonyClark@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 5, FirstName = "Chris", LastName = "Rhea", Email = "ChrisRhea@gmail.com", Username = "TheCrazyChris", Password = "ChrisRhea", Role = "Admin", Description = "I am an admin with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 6, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", Username = "alice123", Password = "AliceSmith@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 7, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@example.com", Username = "bob123", Password = "BobJohnson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 8, FirstName = "Emma", LastName = "Davis", Email = "emma.davis@example.com", Username = "emma123", Password = "EmmaDavis@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 9, FirstName = "Michael", LastName = "Wilson", Email = "michael.wilson@example.com", Username = "michael123", Password = "MichaelWilson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 10, FirstName = "Sophia", LastName = "Thomas", Email = "sophia.thomas31231312@example.com", Username = "sophia123", Password = "SophiaThomas@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 11, FirstName = "Jacob", LastName = "Brown", Email = "jacob.brown@example.com", Username = "jacob123", Password = "JacobBrown@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 12, FirstName = "Olivia", LastName = "Taylor", Email = "olivia.taylor@example.com", Username = "olivia123", Password = "OliviaTaylor@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 13, FirstName = "Ethan", LastName = "Anderson", Email = "ethan.anderson@example.com", Username = "ethan123", Password = "EthanAnderson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 14, FirstName = "Oliver", LastName = "Wilson", Email = "oliver.wilson@example.com", Username = "oliver123", Password = "OliverWilson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 15, FirstName = "Amelia", LastName = "Brown", Email = "amelia.brown@example.com", Username = "amelia123", Password = "AmeliaBrown@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 16, FirstName = "Liam", LastName = "Taylor", Email = "liam.taylor@example.com", Username = "liam123", Password = "LiamTaylor@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 17, FirstName = "Sophia", LastName = "Thomas", Email = "sophia.thomas@example.com", Username = "sophia123", Password = "SophiaThomas@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 18, FirstName = "Noah", LastName = "Clark", Email = "noah.clark@example.com", Username = "noah123", Password = "NoahClark@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 19, FirstName = "Ava", LastName = "Anderson", Email = "ava.anderson@example.com", Username = "ava123", Password = "AvaAnderson@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 20, FirstName = "Mason", LastName = "Miller", Email = "mason.miller@example.com", Username = "mason123", Password = "MasonMiller@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 21, FirstName = "Isabella", LastName = "Martin", Email = "isabella.martin@example.com", Username = "isabella123", Password = "IsabellaMartin@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow },
                new User { ID = 22, FirstName = "James", LastName = "Walker", Email = "james.walker@example.com", Username = "james123", Password = "JamesWalker@", Role = "User", Description = "I am a user with a simple description.", RegisteredAt = DateTime.UtcNow }
            };

            modelBuilder.Entity<User>().HasData(users);

            List<Post> posts = new List<Post>
            {
                new Post{ID = 1,UserID = 1, Title = "Hollywood is the best place to live.", Content = "Let's discuss the suburbs, downtown and Venice beach.", CreatedAt = DateTime.UtcNow},
                new Post{ID = 2,UserID = 2, Title = "Bio fresh food market on Sundays in Virginia?", Content = "Where in VA I can find a fresh food market?", CreatedAt = DateTime.UtcNow},
                new Post{ID = 3,UserID = 3, Title = "Best bars in the downtown DC?", Content = "Guys, please share your experience and let me know which is your favorite place to go out.", CreatedAt = DateTime.UtcNow},
                new Post{ID = 4,UserID = 4, Title = "Football is my life.", Content = "Who are your favorite players teams and etc.", CreatedAt = DateTime.UtcNow},
                new Post { ID = 5, UserID = 5, Title = "Traveling Tips for Europe", Content = "Share your best travel tips and recommendations for visiting Europe.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 6, UserID = 6, Title = "New Movie Releases", Content = "Discuss the latest movies hitting the theaters and share your reviews.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 7, UserID = 7, Title = "Favorite Recipes", Content = "Let's exchange our favorite recipes and cooking techniques.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 8, UserID = 8, Title = "Book Recommendations", Content = "Looking for some new books to read. Any recommendations?", CreatedAt = DateTime.UtcNow },
                new Post { ID = 9, UserID = 9, Title = "Fitness Tips and Motivation", Content = "Share your fitness journey, tips, and motivational stories.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 10, UserID = 10, Title = "Technology Trends", Content = "Discuss the latest trends and advancements in the world of technology.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 11, UserID = 11, Title = "Music Recommendations", Content = "What are you currently listening to? Share your favorite music.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 12, UserID = 12, Title = "Outdoor Adventures", Content = "Share your outdoor adventures and recommend the best hiking trails.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 13, UserID = 13, Title = "Home Improvement Ideas", Content = "Discuss home improvement projects and share your before/after pictures.", CreatedAt = DateTime.UtcNow },
                new Post { ID = 14, UserID = 14, Title = "Gaming Community", Content = "Join the discussion about the latest games and gaming news.", CreatedAt = DateTime.UtcNow },
            };

            modelBuilder.Entity<Post>().HasData(posts);

            List<Comment> comments = new List<Comment>
            {
                new Comment{ID=1 ,UserID = 4, PostID = 1, Message = "I don't like Hollywood at all, there are many homeless people there.", CreatedAt = DateTime.UtcNow},
                new Comment{ID=2 ,UserID = 3, PostID = 1, Message = "I fucking love Venice and Santa Monica. The sunsets there are fucking awesome.", CreatedAt = DateTime.UtcNow},
                new Comment{ID=3 ,UserID = 1, PostID = 1, Message = "Downtown sucks, however I was enjoyed Venice as well.", CreatedAt = DateTime.UtcNow},
                new Comment{ID=4 ,UserID = 1, PostID = 2, Message = "There is one in Springfield near the 7 eleven."},
                new Comment{ID=5 ,UserID = 4, PostID = 2, Message = "Bro, try the one in Alexandria. Food there is always fresh and nice.", CreatedAt = DateTime.UtcNow},
                new Comment{ID=6 ,UserID = 1, PostID = 3, Message = "You  should visit the Off the record bar.", CreatedAt = DateTime.UtcNow},
                new Comment{ID= 7,UserID = 2, PostID = 3, Message = "Any bar in the downtown is pretty fine to me.", CreatedAt = DateTime.UtcNow},
                new Comment { ID = 8, UserID = 6, PostID = 1, Message = "Venice Beach is indeed beautiful. I love the vibrant atmosphere.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 9, UserID = 7, PostID = 1, Message = "Downtown has its charm, but I agree that Venice is more enjoyable.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 10, UserID = 8, PostID = 2, Message = "I've heard great things about the fresh food market in Arlington.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 11, UserID = 9, PostID = 2, Message = "Alexandria also has a nice farmers' market. Check it out!", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 12, UserID = 10, PostID = 3, Message = "The Pub is my favorite bar in downtown DC. Great drinks and atmosphere.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 13, UserID = 11, PostID = 3, Message = "I enjoy trying different bars in the area. There are so many options!", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 14, UserID = 12, PostID = 4, Message = "Football is an amazing sport. I'm a huge fan of the NFL.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 15, UserID = 13, PostID = 4, Message = "My favorite team is the New England Patriots. They always deliver.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 16, UserID = 14, PostID = 5, Message = "Europe is a dream destination for many travelers. Can't wait to explore it!", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 17, UserID = 1, PostID = 5, Message = "Don't forget to visit Paris and Rome. They have amazing landmarks.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 18, UserID = 2, PostID = 6, Message = "The new Avengers movie is a must-watch. It's action-packed!", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 19, UserID = 3, PostID = 6, Message = "I'm more into indie films. Any recommendations for those?", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 20, UserID = 4, PostID = 7, Message = "The rooftop bar at W Hotel has stunning views of the city.", CreatedAt = DateTime.UtcNow },
                new Comment { ID = 21, UserID = 5, PostID = 7, Message = "I enjoy the lively atmosphere at the Adams Morgan bars.", CreatedAt = DateTime.UtcNow },
            };

            modelBuilder.Entity<Comment>().HasData(comments);

            var groups = new List<Group>
    {
        new Group { ID = 1, Name = "Travel Enthusiasts", CreatorID = 1, Description = "A group for passionate travelers.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 2, Name = "Food Lovers", CreatorID = 2, Description = "For those who appreciate good food.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 3, Name = "Fitness Fanatics", CreatorID = 3, Description = "Get fit and healthy together.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 4, Name = "Book Club", CreatorID = 6, Description = "Discuss your favorite books and authors.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 5, Name = "Movie Buffs", CreatorID = 6, Description = "Share your thoughts on the latest movies.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 6, Name = "Tech Geeks", CreatorID = 6, Description = "Explore the latest in technology.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 7, Name = "Sports Fans", CreatorID = 6, Description = "For those who love sports and competition.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 8, Name = "Art and Culture", CreatorID = 8, Description = "Appreciate various forms of art and culture.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 9, Name = "Photography Enthusiasts", CreatorID = 9, Description = "Capture and share stunning photographs.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 10, Name = "Nature Explorers", CreatorID = 10, Description = "Discover the beauty of the natural world.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 11, Name = "Fashion and Style", CreatorID = 9, Description = "Stay updated with the latest fashion trends.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 12, Name = "Music Lovers", CreatorID = 12, Description = "Discuss your favorite genres and artists.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 13, Name = "Gaming Community", CreatorID = 13, Description = "Connect with fellow gamers.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 14, Name = "Cooking Enthusiasts", CreatorID = 13, Description = "Share recipes and cooking tips.", CreatedAt = DateTime.UtcNow },
        new Group { ID = 15, Name = "Outdoor Adventures", CreatorID = 13, Description = "Embark on thrilling outdoor activities.", CreatedAt = DateTime.UtcNow }
    };

            modelBuilder.Entity<Group>().HasData(groups);

            //List<Like> likes = new List<Like>
            //{
            //    new Like{UserID = 4, PostID =3},
            //    new Like{UserID = 1, PostID =3},
            //    new Like{UserID = 2, PostID =3},
            //    new Like{UserID = 1, PostID =2},
            //    new Like{UserID = 2, PostID =2},
            //    new Like{UserID = 3, PostID =1}
            //};

            //modelBuilder.Entity<Like>().HasData(likes);

            List<Tag> tags = new List<Tag>
            {
                new Tag{ ID =1,Name = "outdoor"},
                new Tag{ ID =2,Name = "fresh"},
                new Tag{ ID =3,Name = "downtown"},
                new Tag{ ID =4,Name = "football"},
                new Tag{ ID =5,Name = "favorite"},
                new Tag{ ID =6,Name = "food"},
                new Tag{ ID =7,Name = "market"},
                new Tag{ ID =8,Name = "bar"},
                new Tag{ ID =9,Name = "beach"},
                new Tag{ ID =10,Name = "suburbs"},
                new Tag{ ID =11,Name = "player"},
                new Tag{ ID =12,Name = "team"},
                new Tag{ ID =13,Name = "bio"},
                new Tag{ ID =14,Name = "around"},
                new Tag{ ID =15,Name = "drinks"},
            };

            modelBuilder.Entity<Tag>().HasData(tags);

            List<PostTag> postTags = new List<PostTag>
            {
                new PostTag{ PostID = 1, TagID = 1},
                new PostTag{ PostID = 1, TagID = 3},
                new PostTag{ PostID = 1, TagID = 9},
                new PostTag{ PostID = 1, TagID = 10},
                new PostTag{ PostID = 2, TagID = 2},
                new PostTag{ PostID = 2, TagID = 6},
                new PostTag{ PostID = 2, TagID = 7},
                new PostTag{ PostID = 3, TagID = 3},
                new PostTag{ PostID = 3, TagID = 8},
                new PostTag{ PostID = 3, TagID = 14},
            };

            modelBuilder.Entity<PostTag>().HasData(postTags);
        }
    }
}
