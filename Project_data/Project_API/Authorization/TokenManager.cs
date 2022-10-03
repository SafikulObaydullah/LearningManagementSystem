using Microsoft.IdentityModel.Tokens;
using Project_data.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Project_API.Authorization
{
    public class TokenManager
    {
        private static string secret = "PowerSOftITTrainigPowerSOftITTrainig";
        public static string GenerateToken(UserVM userVM)
        {
            //Subject = new System.Security.Claims.ClaimsIdentity(claims: new[] { new Claim(type: ClaimTypes.Name, value: uName) }),
            byte[] key = Convert.FromBase64String(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
                       {
                            new Claim("UserName", userVM.UserName),
                            new Claim("Email",userVM.Email),
                            //new Claim("PhoneNumber",userVM.p),
                            new Claim("Roles",userVM.RolesName),
                            new Claim("Id",userVM.UserID)
                        });


            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        internal static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwttoken = (JwtSecurityToken)tokenHandler.ReadToken(token);

                if (jwttoken == null)
                {
                    return null;
                }
                byte[] key = Convert.FromBase64String(secret);
                TokenValidationParameters parameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;

            }
        }
    }
}