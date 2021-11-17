using Project2Backend.Context;
using Project2Backend.Models;

namespace Project2Backend.Components;

public class UserImageComponent: Repository<UserImage>
{
    public UserImageComponent(Project2Context project2Context) : base(project2Context)
    {
    }
}