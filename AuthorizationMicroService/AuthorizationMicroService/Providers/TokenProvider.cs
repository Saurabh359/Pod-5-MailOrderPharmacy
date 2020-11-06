using AuthorizationMicroService.Models;
using AuthorizationMicroService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Providers
{
    public class TokenProvider : ITokenProvider<MemberDetails>
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenProvider));

        private ITokenRepository<MemberDetails> tokenRepository;

        private readonly List<MemberDetails> members = new List<MemberDetails>()
        {
            new MemberDetails{Id=1, Name="Saurabh", Email="sm123@gmail.com", Password="hello", Location="Haldwani"},
            new MemberDetails{Id=2, Name="Ben", Email="bt123@gmail.com", Password="hello", Location="Haldwani"}
        };

        public TokenProvider(ITokenRepository<MemberDetails> tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }
        public UserData GetToken(MemberDetails entity)
        {
            _log4net.Debug("Validate Login Details Email- "+entity.Email+" and Password- "+entity.Password);
            try
            {
              var res = members.Find(x => x.Email.Equals(entity.Email) && x.Password.Equals(entity.Password));

                if (res == null)
                    return null;

                _log4net.Info("login data is correct for Email- " + entity.Email + " and Password- " + entity.Password);
                var s=tokenRepository.GetToken(res);
                return s;
            }
            catch (Exception e)
            {
                _log4net.Error("Error occured from " + nameof(TokenProvider) + "Error Message " + e.Message);
                return null;
            }
        }
    }
}
