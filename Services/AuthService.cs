using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using URL_Shortener.Auth;
using Url_Shortener.Dtos;
using URL_Shortener.Entities;

namespace URL_Shortener.Services;

public class AuthService(PasswordHasher<User> passwordHasher, TokenProvider tokenProvider)
{
    public SecurityToken? Handle(User user, UserDtoRequest request)
    {
        var veriedPassword = passwordHasher.VerifyHashedPassword(user, user.password, request.Password);
        if (veriedPassword == PasswordVerificationResult.Failed) return null;
        var token = tokenProvider.CreateToken(user);
        return token;
    }
}