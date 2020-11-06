using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.Providers;
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
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        private ITokenProvider<MemberDetails> tokenProvider;

        public TokenController(ITokenProvider<MemberDetails> _tokenProvider)
        {
            tokenProvider = _tokenProvider; 
        }
        [HttpPost]
        public IActionResult Post([FromBody]MemberDetails userData)
        {

            if (userData == null || String.IsNullOrEmpty(userData.Email) || String.IsNullOrEmpty(userData.Password) )
            {
                _log4net.Warn("Bad request !!!  user data is null");

                return BadRequest("Data is Null or Empty");
            }

            var res = tokenProvider.GetToken(userData);

            if(res==null)
            {
                _log4net.Warn("Bad Request !! Invalid Login Credentials");
                return BadRequest("Invalid credentials");
            }

            return Ok(res);
        }

    }
}
