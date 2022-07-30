// dotnet ef --startup-project ../../KuzApps/ migrations add init --context KuzAppsDbContext

namespace KuzApps.Infra.Data;

public class KuzAppsDbContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    public KuzAppsDbContext(DbContextOptions<KuzAppsDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Comment>()
           .HasOne(c => c.Post)
           .WithMany(p => p.Comments)
           .OnDelete(DeleteBehavior.ClientNoAction);
    }
}