// dotnet ef --startup-project ../../KuzApps/ migrations add init --context KuzAppsDbContext

namespace KuzApps.Infra.Data;

public class KuzAppsDbContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostCategory> PostCategories => Set<PostCategory>();
    public DbSet<PostComment> PostComments => Set<PostComment>();
    public DbSet<PostTag> PostTags => Set<PostTag>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteComment> NoteComments => Set<NoteComment>();

    public KuzAppsDbContext(DbContextOptions<KuzAppsDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PostComment>()
           .HasOne(c => c.Post)
           .WithMany(p => p.Comments)
           .OnDelete(DeleteBehavior.ClientNoAction);

        builder.Entity<NoteComment>()
           .HasOne(c => c.Note)
           .WithMany(p => p.Comments)
           .OnDelete(DeleteBehavior.ClientNoAction);
    }
}