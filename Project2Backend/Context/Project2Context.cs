using Microsoft.EntityFrameworkCore;
using Project2Backend.Controllers;
using Project2Backend.Models;

namespace Project2Backend.Context;

public class Project2Context : DbContext
{
    public Project2Context(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
}
