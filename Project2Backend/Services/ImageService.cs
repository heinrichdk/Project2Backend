using Project2Backend.Components;
using Project2Backend.Models;
using Project2Backend.Models.Requests;

namespace Project2Backend.Services;

public class ImageService
{
    private readonly ImageComponent _imageComponent;
    private readonly UserImageComponent _userImageComponent;

    public ImageService(
        ImageComponent imageComponent,
        UserImageComponent userImageComponent)
    {
        _imageComponent = imageComponent;
        _userImageComponent = userImageComponent;
    }

    public async Task<Project2Response<string>> SaveImageAsync(SaveImageRequest saveImageRequest)
    {
        var response = new Project2Response<string>();
        try
        {
            var image = new Image()
            {
                Name = saveImageRequest.Name,
                DateCreated = DateTimeOffset.Now,
                Tags = saveImageRequest.Tags,
                CapturedBy = saveImageRequest.CapturedBy,
                DateCaptured = saveImageRequest.CapturedDate
            };
            var imageId = await _imageComponent.CreateAsync(image);
            var userImage = new UserImage()
            {
                DateCreated = DateTimeOffset.Now,
                UserId = Guid.Parse(saveImageRequest.UserId),
                ImageId = imageId,
                IsUploader = true
            };
            await _userImageComponent.CreateAsync(userImage);
            response.Success = true;
            response.Result = imageId.ToString();

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying to save image";
        }

        return response;
    }
    
}