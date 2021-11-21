using Microsoft.AspNetCore.Mvc;
using Project2Backend.Models;
using Project2Backend.Models.Requests;
using Project2Backend.Models.Responses;
using Project2Backend.Services;

namespace Project2Backend.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ImageController:ControllerBase
{
    
    private readonly ImageService _imageService;

    public ImageController(ImageService imageService)
    {
        _imageService = imageService;
    }
    
    [HttpPost("save")]
    public async Task<Project2Response<string>> Save(SaveImageRequest saveImageRequest)
    {
        return await _imageService.SaveImageAsync(saveImageRequest);
    }
    
    [HttpGet("images-by-user/{userId}")]
    public  async  Task<Project2Response<List<UserImage>>> GetImagesByUser(Guid userId)
    {
        return await _imageService.GetImagesByUserAsync(userId);
    }

    [HttpPost("update-image")]
    public  async  Task<Project2Response> UpdateImage(Image image)
    {
        return await _imageService.UpdateImageAsync(image);
    }
    
    [HttpPost("delete-image/{imageId}")]
    public  async  Task<Project2Response> DeleteImage(Guid imageId)
    {
        return await _imageService.DeleteImageAsync(imageId);
    }

    [HttpGet("get-shared-users/{imageId}")]
    public async Task<Project2Response<List<UserSmallResponse>>> GetSharedUsers(Guid imageId)
    {
        return await _imageService.GetSharedUsersAsync(imageId);
    }
    
    [HttpPost("share-image")]
    public async Task<Project2Response> ShareImage(ShareImageRequest request)
    {
        return await _imageService.ShareImage(request);
    }
    
    [HttpPost("un-share-image")]
    public async Task<Project2Response> UnShareImage(ShareImageRequest request)
    {
        return await _imageService.UnShareImage(request);
    }
}