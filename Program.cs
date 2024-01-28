
using Forum_Management_System.Data;
using Forum_Management_System.Repositories;
using Forum_Management_System.Services;
using AutoMapper;
using Forum_Management_System.Helpers.Mapper;
using Microsoft.EntityFrameworkCore;
using Forum_Management_System.Services.Interfaces;
using Forum_Management_System.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Forum_Management_System.Helpers.Resolvers;

namespace Forum_Management_System

{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddDbContext<ForumDbContext>(options =>
            {
                // A connection string for establishing a connection to the locally installed SQL Server Express.
                string connectionString = @"Server=localhost;Database=ForumManagementSystem;Trusted_Connection=True;TrustServerCertificate=true";

                // Configure the application to use the locally installed SQL Server Express.
                options.UseSqlServer(connectionString);
                // The following helps with debugging the trobled relationship between EF and SQL \_(-_-)_/ 
                options.EnableSensitiveDataLogging();
            });

            // Add services to the container.
            builder.Services.AddControllers()
                        .AddNewtonsoftJson(options =>
                                            options.SerializerSettings.ReferenceLoopHandling =
                                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Repository
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IPostsRepository, PostsRepository>();
            builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
            builder.Services.AddScoped<ITagsRepository, TagsRepository>();
            builder.Services.AddScoped<IGroupsRepository, GroupRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //Services
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IPostsService, PostsService>();
            builder.Services.AddScoped<ICommentsService, CommentsService>();
            builder.Services.AddScoped<ITagsService, TagsService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<CommentCountResolver>();
            builder.Services.AddScoped<PostsCountResolver>();
            builder.Services.AddScoped<UserLikesResolver>();
            builder.Services.AddScoped<UserDislikesResolver>();

            //Helpers
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,

                                    ValidIssuer = configuration["Jwt:Issuer"],
                                    ValidAudience = configuration["Jwt:Audience"],
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                                    RoleClaimType = "IsAdmin"
                                };

                                options.Events = new JwtBearerEvents
                                {
                                    OnMessageReceived = context =>
                                    {
                                        // Read the token from the request cookie
                                        var token = context.Request.Cookies["X-Access"];

                                        // Set the token as the Bearer token for authentication
                                        context.Token = token;

                                        return Task.CompletedTask;
                                    }
                                };
                            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            //MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapperProfile());
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //builder.Services.AddSingleton(mapper);
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                name: "register",
                pattern: "Register/{action=Register}",
                defaults: new { controller = "Register", action = "Register" }
                );

                endpoints.MapControllerRoute(
         name: "IsEmailUnique",
         pattern: "Register/{action=IsEmailUnique}",
         defaults: new { controller = "Register", action = "IsEmailUnique" }
     );
                endpoints.MapControllerRoute(
         name: "IsPhoneUnique",
         pattern: "Register/{action=IsPhoneUnique}",
         defaults: new { controller = "Register", action = "IsPhoneUnique" }
     );
                endpoints.MapControllerRoute(
         name: "IsUsernameUnique",
         pattern: "Register/{action=IsUsernameUnique}",
         defaults: new { controller = "Register", action = "IsUsernameUnique" }
     );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{action=Index}",
                    defaults: new { controller = "Home" }
                );

                endpoints.MapControllerRoute(
                name: "login",
                pattern: "Login/{action=Login}",
                defaults: new { controller = "Login", action = "Login" }
                );

                endpoints.MapControllerRoute(
                name: "postsPartial",
                pattern: "Home/{action=GetPostsPartial}",
                defaults: new { controller = "Home", action = "GetPostsPartial" }
                );

                endpoints.MapControllerRoute(
                name: "usersPartial",
                pattern: "Home/{action=GetUsersPartial}",
                defaults: new { controller = "Home", action = "GetUsersPartial" }
                );

                endpoints.MapControllerRoute(
                name: "chatMain",
                pattern: "Chat/{action=Index}",
                defaults: new { controller = "Chat", action = "Index" }
                );

                endpoints.MapControllerRoute(
              name: "profile",
              pattern: "User/{action=Profile}",
              defaults: new { controller = "User", action = "Profile" }
              );
                endpoints.MapControllerRoute(
               name: "AddImage",
               pattern: "User/{action=AddImage}",
               defaults: new { controller = "User", action = "AddImage" }
               );

                //endpoints.MapControllerRoute(
                //name: "UserPosts",
                //pattern: "User/Preview/{userId}/GetUserPostsPartial",
                //defaults: new { controller = "User", action = "GetUserPostsPartial" }
                //);

                endpoints.MapDefaultControllerRoute();
            });

            app.MapControllers();

            app.Run();
        }
    }
}