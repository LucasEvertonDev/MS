﻿using Microsoft.IdentityModel.Tokens;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MS.Services.Auth.Infra.Plugins.TokenJWT;

public class TokenService : ITokenService
{
    public Task<string> GenerateToken(User user, List<Role> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("DEFINIR CHAVE");

        user.Password = "";
        user.PasswordHash = "";

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var role in roles)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role?.Name));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}
