using Project2Backend.Components;
using Project2Backend.Models;
using Project2Backend.Models.Requests;
using Project2Backend.Models.Responses;

namespace Project2Backend.Services;

public class ImageService
{
    private readonly ImageComponent _imageComponent;
    private readonly UserImageComponent _userImageComponent;
    private readonly  UserComponent _userComponent;

    public ImageService(
        ImageComponent imageComponent,
        UserImageComponent userImageComponent,
        UserComponent userComponent)
    {
        _imageComponent = imageComponent;
        _userImageComponent = userImageComponent;
        _userComponent = userComponent;
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
                DateCaptured = saveImageRequest.CapturedDate,
                Type = saveImageRequest.Type
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
    
    
    public async Task<Project2Response> UpdateImageAsync(Image saveImageRequest)
    {
        var response = new Project2Response();
        try
        {
            var image =  await _imageComponent.GetAsync(saveImageRequest.Id);

            
            image.Name = saveImageRequest.Name;
            image.Tags = saveImageRequest.Tags;
            image.CapturedBy = saveImageRequest.CapturedBy;
            image.DateCaptured = saveImageRequest.DateCaptured;
            await _imageComponent.UpdateAsync(image);
         
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying to update image meta data";
        }

        return response;
    }

    public async Task<Project2Response<List<UserImage>>> GetImagesByUserAsync(Guid userId)
    {
        var response = new Project2Response<List<UserImage>>();
        try
        {
            var userImages = await _userImageComponent.GetImagesByUserId(userId);
            response.Success = true;
            response.Result =  userImages;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying to save image";
        }

        return response;
    }

    public async Task<Project2Response> DeleteImageAsync(Guid imageId)
    {
        
        var response = new Project2Response();
        try
        {
            var userImages =  await _userImageComponent.GetImagesByImageId(imageId);
            await _userImageComponent.DeleteListAsync(userImages);

            await _imageComponent.DeleteAsync(imageId);
            
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying to save image";
        }

        return response;
      
    }

    public async Task<Project2Response<List<UserSmallResponse>>> GetSharedUsersAsync(Guid imageId)
    {
        var response = new Project2Response<List<UserSmallResponse>>();
        try
        {
            var userImages =  await _userImageComponent.GetSharedUsersByImageId(imageId);
            var list = new List<UserSmallResponse>();
            foreach (var userImage in userImages)
            {
                var user = new UserSmallResponse()
                {
                    Id = userImage.UserId,
                    Username = userImage.User.Username
                };
                list.Add(user);
            }
            response.Result = list;
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying get shared users";
        }

        return response;
    }


    public async Task<Project2Response> ShareImage(ShareImageRequest request)
    {
        var response = new Project2Response();
        try
        {
            var user = await _userComponent.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                response.Message = "User does not exist please make sure sure is registered";
                return response;
            }

            var userImage = await _userImageComponent.GetUserImageByUserAndImage(request.ImageId, user.Id);
            if (userImage != null)
            {
                response.Message = "User already have access to that Image";
                return response;
            }
            
            
            var userImageNew = new UserImage()
            {
                DateCreated = DateTimeOffset.Now,
                UserId = user.Id,
                ImageId = request.ImageId,
                IsUploader = false
            };
            await _userImageComponent.CreateAsync(userImageNew);
            
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying get shared users";
        }

        return response;
    }
    
    
    
    public async Task<Project2Response> UnShareImage(ShareImageRequest request)
    {
        var response = new Project2Response();
        try
        {
            var user = await _userComponent.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                response.Message = "User does not exist please make sure sure is registered";
                return response;
            }

            var userImage = await _userImageComponent.GetUserImageByUserAndImage(request.ImageId, user.Id);
            if (userImage == null)
            {
                response.Message = "Record not found please refresh page";
                return response;
            }
            
            
            await _userImageComponent.DeleteAsync(userImage);
            
            response.Success = true;

        }
        catch (Exception e)
        {
            response.Message = "An unexpected error has occurred while trying get shared users";
        }

        return response;
    }
}