using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationMicroService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));

        private readonly List<MemberDetails> members = new List<MemberDetails>()
        {
            new MemberDetails{Id=1, Name="Saurabh", Email="sm123@gmail.com", Password="hello", Location="Haldwani"},
            new MemberDetails{Id=2, Name="Ben", Email="bt123@gmail.com", Password="hello", Location="Haldwani"}
        };

        public IConfiguration configuration;

        public TokenController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpPost]
        public IActionResult Post([FromBody]MemberDetails userData)
        {
            _log4net.Debug("Authentication initiated");

            if (userData != null && userData.Email != null && userData.Password != null)
            {
                var user = GetMember(userData.Email, userData.Password);

                if (user != null)
                {
                    _log4net.Info("login data is correct");

                    _log4net.Debug("Token generation initiated");

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
                        Expires = DateTime.UtcNow.AddMinutes(10),
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

                    _log4net.Debug("Returning token with user data");
                    return Ok(data);
                }
                else
                {
                    _log4net.Warn("Bad Request !! Invalid Login Credentials");

                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                _log4net.Warn("Bad request !!!  user data is null");

                return BadRequest();
            }
        }

        private MemberDetails GetMember(string email, string password)
        {
            _log4net.Debug("Validate Login Details");
            var res = members.Find(x => x.Email.Equals(email) && x.Password.Equals(password));
            return res;
        }

    }
}
