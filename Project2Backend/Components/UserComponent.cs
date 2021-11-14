using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Project2Backend.Context;
using Project2Backend.Models;

namespace Project2Backend.Components;

public class UserComponent : Repository<User>
{
    public UserComponent(Project2Context project2Context) : base(project2Context)
    {
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await Project2Context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}