using Mensageria.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mensageria.Infra.db.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<MessageRecipientEntity> Recipients { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}