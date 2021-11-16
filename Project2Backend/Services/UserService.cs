using System.Security.Claims;
using Project2Backend.Components;
using Project2Backend.Models;
using Project2Backend.Models.Requests;
using Project2Backend.Models.Responses;
using Project2Backend.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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

    public async Task<Project2Response<List<User>>> GetAllUsers()
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

    public async Task<Project2Response<SignInResponse>> SignUpUserAsync(SignUpRequest signUpRequest)
    {
        var response = new Project2Response<SignInResponse>();
        var responseDto = new SignInResponse();

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
                DateCreated = DateTimeOffset.Now
            };

            var id = await _userComponent.CreateAsync(user);

            responseDto.Id = id.ToString();
            responseDto.Username = signUpRequest.Username;
        }
        catch (Exception ex)
        {
            var message = "Failed to sign up user";

            response.Message = message;
            return response;
        }

        response.Result = responseDto;
        response.Success = true;

        return response;
    }

    public async Task<Project2Response<SignInResponse>> SignInUserAsync(SignInRequest signInRequest)
    {
        var response = new Project2Response<SignInResponse>();

        try
        {

            var existingUser = await _userComponent.GetUserByUsernameAsync(signInRequest.Username);

            if (existingUser == null)
            {
                response.Message = "Invalid email or password";
                return response;
            }

            if (!_cryptoComponent.Verify(existingUser.Salt, existingUser.Password, signInRequest.Password))
            {
                response.Message = "Invalid email or password";
                return response;
            }
            
            response.Result = new SignInResponse
            {
                Username = existingUser.Username,
                Id = existingUser.Id.ToString()
            };
        }
        catch (Exception ex)
        {
            var message = "Failed to sign in user";

            response.Message = message;
            return response;
        }
        
        response.Success = true;

        return response;
    }
    
}