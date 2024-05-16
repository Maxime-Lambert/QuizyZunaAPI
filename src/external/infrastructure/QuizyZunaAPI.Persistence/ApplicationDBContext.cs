using Microsoft.EntityFrameworkCore;

using QuizyZunaAPI.Application;
using QuizyZunaAPI.Domain.Questions;
using QuizyZunaAPI.Domain.Questions.Enumerations;

namespace QuizyZunaAPI.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresEnum<Difficulty>();
        modelBuilder.HasPostgresEnum<Era>();
        modelBuilder.HasPostgresEnum<Topic>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
