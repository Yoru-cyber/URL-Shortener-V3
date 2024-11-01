using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Context;
using Url_Shortener.Dtos;
using URL_Shortener.Entities;
using URL_Shortener.Services;

namespace URL_Shortener.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController(ShortenedUrlContext context, AuthService authService, PasswordHasher<User> passwordHasher)
    : ControllerBase
{
    //api/v1/Auth/signup
    //Reads request username and password field from body, creates user and saves it to database
    [HttpPost("signup")]
    public async Task<ActionResult> SignUpAsync([FromBody] UserDtoRequest request)
    {
        var user = new User { username = request.Username, password = request.Password, email = request.Email };
        var hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.password = hashedPassword;
        var savedUser = context.Add(user);
        var response = new UserDtoResponse(savedUser.Entity.Id, savedUser.Entity.email, savedUser.Entity.username);
        var saveTask = await context.SaveChangesAsync();
        if (saveTask > 0)
            return
                Ok(response);
        return Conflict("Could not create user");
    }

    //api/v1/Auth/signup
    //Reads request with credentials from body, passes it to authService and return the security token or an Unauthorized response
    [HttpPost("login")]
    public async Task<IActionResult> Handle(UserDtoRequest request)
    {
        var user = await context.Users.Where(user => user.email == request.Email).FirstOrDefaultAsync();
        if (user == null) return Conflict("User does not exist");
        var token = authService.Handle(user, request);
        if (token == null) return Unauthorized("Password or email incorrect");
        return Ok(token);
    }

    [HttpGet("test")]
    [Authorize]
    public async Task<IActionResult> test()
    {
        return Ok("You are logged in");
    }
}