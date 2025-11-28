using BackgroundServiceMath.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackgroundServiceMath.Data;

public class BackgroundServiceContext : IdentityDbContext
{
    public BackgroundServiceContext(DbContextOptions<BackgroundServiceContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Player { get; set; } = default!;
}