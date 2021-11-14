using AutoMapper;
using Project2Backend.Components;
using Project2Backend.Models;
using Project2Backend.Models.Requests;
using Project2Backend.Models.Responses;
using Project2Backend.Validators;

namespace Project2Backend.Services;

public class UserService
{
    private readonly UserComponent _userComponent;
    private readonly CryptoComponent _cryptoComponent;

    public UserService(
        UserComponent userComponent,
        CryptoComponent cryptoComponent)
    {
        _userComponent = userComponent;
        _cryptoComponent = cryptoComponent;
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

    public async Task<Project2Response<SignUpResponse>> SignUp(SignUpRequest signUpRequest)
    { 
        var response = new Project2Response<SignUpResponse>();
        var registerResponseDto = new SignUpResponse();

            try
            {
                var validator = new SignUpRequestValidator();
                var validationResult = await validator.ValidateAsync(signUpRequest);

                if (!validationResult.IsValid)
                {
                    response.Message = validationResult.ToString();
                    return response;
                }

                var existingUser = await _userComponent.GetUserByUsernameAsync(signUpRequest.Username);

                if (existingUser != null)
                {
                    response.Message = "This user already exists. Please try again with an alternative username";
                    return response;
                }

                var (salt, hashedPassword) = _cryptoComponent.HashPassword(signUpRequest.Password);
                

                var user = new User
                {
                    Password = hashedPassword,
                    Salt = salt,
                    Name = signUpRequest.Name,
                    Surname = signUpRequest.Surname,
                    Username = signUpRequest.Username,
                };

                await _userComponent.CreateAsync(user);
            }
            catch (Exception ex)
            {
                var message = "Failed to sign up user: " + ex.Message;

                response.Message = message;
                return response;
            }

            response.Result = registerResponseDto;
            response.Success = true;

            return response;
    }
        
}