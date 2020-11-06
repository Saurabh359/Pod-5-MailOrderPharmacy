using AuthorizationMicroService.Models;
using AuthorizationMicroService.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestAuthorizationMicroService
{
    public class TokenRepositoryTesting
    {
        private ITokenRepository<MemberDetails> repository;
        private Mock<IConfiguration> configuration; 

        [SetUp]
        public void SetUp()
        {
            configuration = new Mock<IConfiguration>();
            repository = new TokenRepository(configuration.Object);
        }

        [Test]
        public void GetToken_ReturnUserDataObject_WithJWTToken()
        {
            //Arrange
            MemberDetails arg = new MemberDetails
            {
                Id = 1,
                Name = "Saurabh",
                Email = "sm123@gmail.com",
                Password = "hello",
                Location = "Haldwani"
            };

            configuration.Setup(p => p["Jwt:Key"]).Returns("ssdfddsfffgsdfsfdfgg");
            configuration.Setup(p => p["Jwt:Issuer"]).Returns("InventoryAuthenticationServer");
            configuration.Setup(p => p["Jwt:Audience"]).Returns("InventoryServicePostmanClient");

            //Act

            var result = repository.GetToken(arg);

            //Assert
            Assert.IsNotEmpty(result.Token);
        }

        [TestCase(null,null)]
        [TestCase("Saurabh",null)]
        [TestCase(null,"sm123@gmail.com")]
        public void GetToken_ReturnNull_IFNullNameOrEmailPassed(string name,string email)
        {
            //Arrange
            MemberDetails arg = new MemberDetails
            {
                Id = 1,
                Name = "Saurabh",
                Email = "sm123@gmail.com",
                Password = "hello",
                Location = "Haldwani"
            };

            arg.Name = name;
            arg.Email = email;

            configuration.Setup(p => p["Jwt:Key"]).Returns("ssdfddsfffgsdfsfdfgg");
            configuration.Setup(p => p["Jwt:Issuer"]).Returns("InventoryAuthenticationServer");
            configuration.Setup(p => p["Jwt:Audience"]).Returns("InventoryServicePostmanClient");

            //Act

            var result = repository.GetToken(arg);

            //Assert
            Assert.IsNull(result);
        }
    }
}
