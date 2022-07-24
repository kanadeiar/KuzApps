// dotnet ef --startup-project ../../KuzApps/ migrations add init --context KuzAppsDbContext

namespace KuzApps.Infra.Data;

public class KuzAppsDbContext : IdentityDbContext<User, Role, string>
{
    public KuzAppsDbContext(DbContextOptions<KuzAppsDbContext> options) : base(options)
    { }
}