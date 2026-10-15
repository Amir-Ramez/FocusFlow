using FocusFlow.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Data;

public class AppDbContext : DbContext {
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options) {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Email)
                .HasMaxLength(255)
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.TimeZone)
                .HasMaxLength(50)
                .IsRequired();
        });

        modelBuilder.Entity<Goal>(entity => {
            entity.HasKey(g => g.Id);

            entity.Property(g => g.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId);

            entity.HasIndex(g => g.UserId);
        });

        modelBuilder.Entity<TaskItem>(entity => {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(t => t.Priority)
                .HasConversion<string>();

            entity.Property(t => t.Status)
                .HasConversion<string>();

            entity.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            entity.HasOne(t => t.Goal)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t => t.GoalId)
                .IsRequired(false);

            entity.HasIndex(t => t.UserId);
        });

        modelBuilder.Entity<UserPreference>(entity => {
            entity.HasKey(p => p.Id);

            entity.HasOne(p => p.User)
                .WithOne(u => u.Preference)
                .HasForeignKey<UserPreference>(
                    p => p.UserId);

            entity.HasIndex(p => p.UserId)
                .IsUnique();
        });
    }
}
