using API.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<Quest> Quests { get; set; }
}