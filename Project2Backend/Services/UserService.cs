using AutoMapper;
using Project2Backend.Components;
using Project2Backend.Models;

namespace Project2Backend.Services;

public class UserService
{
    private readonly UserComponent _userComponent;

    public UserService(
        UserComponent userComponent)
    {
        _userComponent = userComponent;
    }

    public async Task<Project2Response<List<User>>> GetAllUser()
    {
        var project2Response = new Project2Response<List<User>>();

        try
        {
            var users = await _userComponent.GetAllAsync();

            project2Response.Result = users.ToList();
            project2Response.Success = true;
        }
        catch (Exception ex)
        {
            project2Response.Message = "An error has occurred while trying to get all user objects";
        }

        return project2Response;
    }
}