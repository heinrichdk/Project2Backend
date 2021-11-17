using Microsoft.AspNetCore.Mvc;
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
    public async Task<Project2Response<string>> SignUpUser(SaveImageRequest saveImageRequest)
    {
        return await _imageService.SaveImageAsync(saveImageRequest);
    }
}