using AuthorizationMicroService.Models;
using AuthorizationMicroService.Providers;
using AuthorizationMicroService.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestAuthorizationMicroService
{
    public class TokenProviderTesting
    {
        private ITokenProvider<MemberDetails> provider;
        private Mock<ITokenRepository<MemberDetails>> repository;
        
        [SetUp]
        public void Setup()
        {
            repository = new Mock<ITokenRepository<MemberDetails>>();
            provider = new TokenProvider(repository.Object);
        }

        [Test]
        public void GetToken_ReturnsNull_OnFalseData()
        {
           //Arrange
            MemberDetails details = new MemberDetails()
            {
                Id =0,
                Name = "",
                Email = "false123@gmail.com",
                Password = "false",
                Location = ""
            };

            UserData userData = null;
            repository.Setup(x => x.GetToken(details)).Returns(userData);

            //Act
            var result = provider.GetToken(details);

            //Assert
            Assert.IsNull(result);

        }

        [Test]
        public void GetToken_ReturnUserDataObject_OnTrueData()
        {
            //Arrange
            MemberDetails details = new MemberDetails
            {
                Id = 0,
                Name = "",
                Email = "sm123@gmail.com",
                Password = "hello",
                Location = ""
            };

            UserData user = new UserData
            { 
                Id = 1,
                Location = "Haldwani",
                Token = "TrileToken"
            };
            repository.Setup(x => x.GetToken(It.IsAny<MemberDetails>())).Returns(user);

            //Act
            var result = provider.GetToken(details);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}