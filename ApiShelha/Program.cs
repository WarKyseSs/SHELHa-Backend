using System.Text;
using Application.Services.Conversation;
using Application.Services.Implantation;
using Application.Services.Role;
using Application.UseCases.UserConnexion;
using Infrastructure.Ef;
using Application.Services.Post;
using Application.Services.Topic;
using Application.Services.User;
using Application.UseCases.Topic;
using Application.UseCases.Comment.Dtos;
using Application.UseCases.Post;
using Application.UseCases.Post.Dtos;
using Application.UseCases.Articles;
using Application.UseCases.Conversations;
using Application.UseCases.Events;
using Application.UseCases.Role;
using Application.UseCases.Message;
using Application.UseCases.Users;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiShelha;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
builder.Services.AddScoped<ShelhaContextProvider>();


// Use cases articles
builder.Services.AddScoped<UseCaseFetchAllArticles>();
builder.Services.AddScoped<UseCaseFetchArticleById>();
builder.Services.AddScoped<UseCaseCreateArticle>();
builder.Services.AddScoped<UseCaseDeleteArticleById>();
builder.Services.AddScoped<UseCaseUpdateArticle>();
builder.Services.AddScoped<UseCaseFetchArticleByUrl>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();


// Use cases events
builder.Services.AddScoped<UseCaseFetchAllEvents>();
builder.Services.AddScoped<UseCaseFetchEventById>();
builder.Services.AddScoped<UseCaseCreateEvent>();
builder.Services.AddScoped<UseCaseDeleteEventById>();
builder.Services.AddScoped<UseCaseUpdateEvent>();
builder.Services.AddScoped<UseCaseFetchEventByUrl>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ShelhaContextProvider>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UseCaseConnectUser>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<SlugUrlProvider>();

// Use cases topic
builder.Services.AddScoped<UseCaseFetchAllTopic>();
builder.Services.AddScoped<UseCaseCreateTopic>();
builder.Services.AddScoped<UseCaseDeleteTopic>();
builder.Services.AddScoped<UseCaseUpdateTopic>();
builder.Services.AddScoped<UseCaseFetchTopicById>();
builder.Services.AddScoped<UseCaseFetchPostsByUrlTopic>();
builder.Services.AddScoped<ITopicService, TopicService>();

// Use cases post
builder.Services.AddScoped<UseCaseCreatePost>();
builder.Services.AddScoped<UseCaseDeletePost>();
builder.Services.AddScoped<UseCaseUpdatePost>();
builder.Services.AddScoped<UseCaseFetchPostByUrl>();
builder.Services.AddScoped<UseCaseFetchCommentsByUrl>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<UseCaseFetchPostByUrl>();
builder.Services.AddScoped<IUserService, UserService>();


// Use cases comment
builder.Services.AddScoped<UseCaseCreateComment>();
builder.Services.AddScoped<UseCaseDeleteComment>();
builder.Services.AddScoped<UseCaseUpdateComment>();

// Use cases users
builder.Services.AddScoped<IImplantationRepository, ImplantationRepository>();
builder.Services.AddScoped<UseCaseFetchAllUsers>();
builder.Services.AddScoped<UseCaseCreateUser>();
builder.Services.AddScoped<UseCaseFetchUserById>();
builder.Services.AddScoped<UseCaseDeleteUser>();
builder.Services.AddScoped<UseCaseUpdateUser>();
builder.Services.AddScoped<UseCaseFetchAllImplantation>();
builder.Services.AddScoped<UseCaseFetchAllRoles>();
builder.Services.AddScoped<IImplantationService, ImplantationService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UseCaseConnectUser>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<UseCaseFetchRoleById>();
builder.Services.AddScoped<UseCaseFetchImplantationById>();
builder.Services.AddScoped<UseCaseFetchUserByUsername>();


//Use case messaging
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<UseCaseCreateMessage>();
builder.Services.AddScoped<UseCaseGetAllMessages>();

//Use case conversations
builder.Services.AddScoped<IConversationRepository, ConversationRepository>();
builder.Services.AddScoped<UseCaseGetConversations>();
builder.Services.AddScoped<UseCaseCreateConversation>();
builder.Services.AddScoped<UseCaseGetConversationBySlug>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<UseCaseGetAllMessageOfConversation>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{        
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["tokenCookie"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Dev");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//messaging
app.MapRazorPages();
app.MapHub<ChatHub>("/chat");

app.Run();