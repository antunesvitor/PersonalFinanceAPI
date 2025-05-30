using Microsoft.EntityFrameworkCore;
using PersonalFinanceAPI.Models;

namespace PersonalFinanceAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Expense> Expenses { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Group>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e=>e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Expense>(entity => 
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Date);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(e => e.Group)
                .WithMany(e => e.Expenses)
                .HasForeignKey(e =>e.GroupID)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
