using AuthorizationMicroService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Repository
{
    public class TokenRepository : ITokenRepository<MemberDetails>
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenRepository));
        private IConfiguration configuration;
        
        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public UserData GetToken(MemberDetails user)
        {
            _log4net.Debug("Token generation initiated for User- "+user.Name);

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"],
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey),
                                                                SecurityAlgorithms.HmacSha256Signature)
                };

                var tokencreate = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tokencreate);

                _log4net.Info("Token Generated Successfuly");

                UserData data = new UserData
                {
                    Id = user.Id,
                    Location = user.Location,
                    Token = token
                };

                _log4net.Debug("Returning token with user data containing Id- "+data.Id+" and Location- "+data.Location+" and Token- "+data.Token);
                return data;
            }
            catch(Exception e)
            {
                _log4net.Error("Error occured from " + nameof(TokenRepository) + "Error Message " + e.Message);
                return null;
            }
        }
    }
}
