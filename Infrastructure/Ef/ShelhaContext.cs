using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class ShelhaContext : DbContext
{
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbRole> Roles { get; set; }
    public DbSet<DbImplantation> Implantations { get; set; }
    public DbSet<DbTopic> Topic { get; set; }
    public DbSet<DbPost> Post { get; set; }
    public DbSet<DbComment> Comment { get; set; }
    public DbSet<DbArticle> Articles { get; set; }
    public DbSet<DbEvent> Events { get; set; }
    
    public DbSet<DbMessage> Messages { get; set; }
    
    public DbSet<DbConversation> Conversations { get; set; }

    private readonly IConnectionStringProvider _connectionStringProvider;

    public ShelhaContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionStringProvider.Get("db"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbTopic>(entity =>
        {
            entity.ToTable("f_categories");
            entity.HasKey(topic => topic.idCat);
            entity.Property(topic => topic.idCat).HasColumnName("id_cat");
            entity.Property(topic => topic.nameCat).HasColumnName("name_cat");
            entity.Property(topic => topic.description).HasColumnName("description");
            entity.Property(topic => topic.urlTopic).HasColumnName("url_cat");
        });
        
        modelBuilder.Entity<DbPost>(entity =>
        {
            entity.ToTable("f_posts");
            entity.HasKey(post => post.idPost);
            entity.Property(post => post.idPost).HasColumnName("id_post");
            entity.Property(post => post.idAuthor).HasColumnName("id_author");
            entity.Property(post => post.idCat).HasColumnName("id_cat");
            entity.Property(post => post.message).HasColumnName("message");
            entity.Property(post => post.datePost).HasColumnName("date_post");
            entity.Property(post => post.dateLastEdit).HasColumnName("date_last_edit");
            entity.Property(post => post.urlPost).HasColumnName("url_post");
        });
        
        modelBuilder.Entity<DbComment>(entity =>
        {
            entity.ToTable("f_comments");
            entity.HasKey(comment => comment.idComment);
            entity.Property(comment => comment.idComment).HasColumnName("id_comment");
            entity.Property(comment => comment.idPost).HasColumnName("id_post");
            entity.Property(comment => comment.idUser).HasColumnName("id_user");
            entity.Property(comment => comment.message).HasColumnName("message");
            entity.Property(comment => comment.dateComment).HasColumnName("date_comment");
            entity.Property(comment => comment.dateLastEdit).HasColumnName("date_last_edit");
        });
        
        modelBuilder.Entity<DbUser>(entity =>
        {
            entity.ToTable("users");
            
            entity.HasKey(user => user.id);
            
            entity.Property(user => user.id).HasColumnName("id_user");
            
            entity.Property(user => user.username).HasColumnName("username");

            entity.Property(user => user.lastname).HasColumnName("lastname");
            
            entity.Property(user => user.firstname).HasColumnName("firstname");

            entity.Property(user => user.mailaddress).HasColumnName("mail_address");
            
            entity.Property(user => user.password).HasColumnName("password");
    
            entity.Property(user => user.idImplantation).HasColumnName("id_implantation");
            
            entity.Property(user => user.connectionDate).HasColumnName("connection_date");
            
            entity.Property(user => user.registrationDate).HasColumnName("registration_date");
            
            entity.Property(user => user.idUserRole).HasColumnName("id_role");

            entity.Property(user => user.validatorkey).HasColumnName("validator_key");
            
            entity.Property(user => user.mailValidation).HasColumnName("mail_validation");

        });
        
        modelBuilder.Entity<DbRole>(entity =>
        {
            entity.ToTable("roles");
            
            entity.HasKey(role => role.idRole);
            
            entity.Property(role => role.idRole).HasColumnName("id_role");

            entity.Property(role => role.nameRole).HasColumnName("name_role");
        });
        
        modelBuilder.Entity<DbImplantation>(entity =>
        {
            entity.ToTable("implantation");
            
            entity.HasKey(implantation => implantation.idImplantation);
            
            entity.Property(implantation => implantation.idImplantation).HasColumnName("id_implantation");

            entity.Property(implantation => implantation.nameImplantation).HasColumnName("name_implantation");
        });
        
        modelBuilder.Entity<DbArticle>(entity =>
        {
            entity.ToTable("articles");
            entity.HasKey(a => a.IdArticle);
            entity.Property(a => a.IdArticle).HasColumnName("id_article");
            entity.Property(a => a.Title).HasColumnName("title");
            entity.Property(a => a.Description).HasColumnName("description");
            entity.Property(a => a.DatePublication).HasColumnName("date_publication_article");
            entity.Property(a => a.IdAuthor).HasColumnName("id_author");
            entity.Property(a => a.urlArticle).HasColumnName("url_article");
        });
        
        modelBuilder.Entity<DbEvent>(entity =>
        {
            entity.ToTable("events");
            entity.HasKey(e => e.IdEvent);
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DatePublication).HasColumnName("date_publication_event");
            entity.Property(e => e.DateEvent).HasColumnName("date_event");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Street).HasColumnName("street");
            entity.Property(e => e.StreetNumber).HasColumnName("street_number");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.urlEvent).HasColumnName("url_event");
        });
        
        modelBuilder.Entity<DbConversation>(entity =>
        {
            entity.ToTable("conversations");
            
            entity.HasKey(conversation => conversation.Id);
            
            entity.Property(conversation => conversation.IdUserOne).HasColumnName("id_user_one");

            entity.Property(conversation => conversation.IdUserTwo).HasColumnName("id_user_two");
            
            entity.Property(conversation => conversation.LastMessage).HasColumnName("last_message_send");
            
            entity.Property(conversation => conversation.Timestamp).HasColumnName("timestamp");
            
            entity.Property(conversation => conversation.Slug).HasColumnName("slug");
            
            entity.Property(conversation => conversation.Subject).HasColumnName("subject");
            
        });
        
        modelBuilder.Entity<DbMessage>(entity =>
        {
            entity.ToTable("private_messages");

            entity.HasKey(message => message.Id);
            
            entity.Property(message => message.IdConversation).HasColumnName("id_conversation");

            entity.Property(message => message.IdSender).HasColumnName("sender_id");
            
            entity.Property(message => message.Message).HasColumnName("message");

            entity.Property(message => message.TimeStamp).HasColumnName("timestamp");
            
            entity.Property(message => message.IsRead).HasColumnName("is_read");

        });


    }
}