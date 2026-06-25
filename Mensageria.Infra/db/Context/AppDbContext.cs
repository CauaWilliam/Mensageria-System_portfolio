using Mensageria.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Mensageria.Infra.db.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

    public DbSet<UserEntity> Users { get; set; }
}