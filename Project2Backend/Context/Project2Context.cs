using Microsoft.EntityFrameworkCore;
using Project2Backend.Models;

namespace Project2Backend.Context;

public class Project2Context : DbContext
{
    public Project2Context(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}
