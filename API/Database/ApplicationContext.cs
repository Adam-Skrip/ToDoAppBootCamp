using API.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestBasket>()
            .HasOne(p => p.User)
            .WithMany(b => b.Baskets).
            HasForeignKey(p => p.UserId);
        
        modelBuilder.Entity<Quest>()
            .HasOne(p => p.Basket)
            .WithMany(b => b.Quests).
            HasForeignKey(p => p.BasketId);
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<QuestBasket> Baskets { get; set; }
    public DbSet<Quest> Quests { get; set; }
    
    
    
    
    
}