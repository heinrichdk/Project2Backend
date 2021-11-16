using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Project2Backend.Models;
using Project2Backend.Models.Requests;
using Project2Backend.Models.Responses;
using Project2Backend.Services;

namespace Project2Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<Project2Response<List<User>>> Get()
    {
        return await _userService.GetAllUsers();
    }

    [HttpPost("sign-up")]
    public async Task<Project2Response<SignInResponse>> SignUpUser(SignUpRequest signUpRequest)
    {
         return await _userService.SignUpUserAsync(signUpRequest);
    }

    [HttpPost("sign-in")]
    public async Task<Project2Response<SignInResponse>> SignInUser(SignInRequest signInRequest)
    {
       return  await _userService.SignInUserAsync(signInRequest);

    }
}