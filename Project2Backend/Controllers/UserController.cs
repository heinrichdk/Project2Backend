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
        var response = await _userService.SignUpUserAsync(signUpRequest);
        if (response.Success)
            await SignUserIn(response.Result);
        return response;
    }

    [HttpPost("sign-in")]
    public async Task<Project2Response<SignInResponse>> SignInUser(SignInRequest signInRequest)
    {
        return await _userService.SignInUserAsync(signInRequest);
    }


    private async Task SignUserIn(SignInResponse signedInUser)
    {
        var claim = new Claim(ClaimTypes.Name, signedInUser.Id.ToString());
        var claimsIdentity = new ClaimsIdentity(new[] {claim}, "serverAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claimsPrincipal);
    }
}