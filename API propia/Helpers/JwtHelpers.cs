﻿using API_propia.Data_Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings;

namespace API_propia.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd yyyy HH:mm:ss tt"))
            };
            
            if(userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, userAccounts.Role = "Administrator")); //If there is an error with roles, change userAccounts.Role = "Administrator" with just "Administrator" and also delete Role property from UserTokens class
            }

            else
            {
                claims.Add(new Claim(ClaimTypes.Role, userAccounts.Role = "User")); //If there is an error with roles, change userAccounts.Role = "User" with just "User"
            }
            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        public static UserTokens GenerateTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();
                
                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //Generate the secret key
                var key = Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                //Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity of our token
                userToken.Validity = expireTime.TimeOfDay;

                //Generate Token
                var JWT = new JwtSecurityToken(
                    
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWT);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuiId = Id;
                return userToken;
            }

            catch(Exception ex)
            {
                throw new Exception("Error generating the JWT", ex);
            }
        }
    }
}
