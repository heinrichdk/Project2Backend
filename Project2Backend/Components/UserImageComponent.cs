using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Project2Backend.Context;
using Project2Backend.Models;

namespace Project2Backend.Components;

public class UserImageComponent : Repository<UserImage>
{
    public UserImageComponent(Project2Context project2Context) : base(project2Context)
    {
    }

    public async Task<List<UserImage>> GetImagesByUserId(Guid userid)
    {
        return await Project2Context.UserImages.Where(x => x.UserId == userid)
            .Include(x => x.Image).Include(x => x.User).ToListAsync();
    }

    public async Task<List<UserImage>> GetImagesByImageId(Guid imageId)
    {
        return await Project2Context.UserImages.Where(x => x.ImageId == imageId).ToListAsync();
    }

    public async Task<List<UserImage>> GetSharedUsersByImageId(Guid imageId)
    {
        return await Project2Context.UserImages.Include(x => x.User).Where(x => x.ImageId == imageId && !x.IsUploader)
            .ToListAsync();
    }

    public async Task<UserImage> GetUserImageByUserAndImage(Guid requestImageId, Guid userId)
    {
        return Project2Context.UserImages
            .FirstOrDefault(x => x.UserId == userId && x.ImageId == requestImageId);
    }
}