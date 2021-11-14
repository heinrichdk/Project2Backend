using Microsoft.AspNetCore.Mvc;
using Project2Backend.Models;
using Project2Backend.Services;

namespace Project2Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController:ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    
    
    [HttpGet]
    public Project2Response<List<User>> Get()
    {
        return null;
    }
}