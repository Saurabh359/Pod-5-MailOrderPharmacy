using AuthorizationMicroService.Controllers;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.Providers;
using AuthorizationMicroService.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTestingOfAuthorizationMicroService
{
    public class Tests
    {
        private ITokenProvider<MemberDetails> provider;
        private Mock<List<MemberDetails>> MockMember;
        private Mock<ITokenRepository<MemberDetails>> repository;

        [SetUp]
        public void Setup()
        {
            MockMember = new Mock<List<MemberDetails>>();
            repository = new Mock<ITokenRepository<MemberDetails>>();
            provider = new TokenProvider(repository.Object);
        }

        [Test]
        public void Test_GetToken_ReturnsNullOnFalseData()
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

            //MemberDetails response = null;
            //MockMember.Setup(x => x.Find(x => x.Email.Equals(details.Email) && x.Password.Equals(details.Password))).Returns(response);

            //Act
            var result = provider.GetToken(details);

            //Assert
            Assert.IsNull(result);

        }

        [Test]
        public void Test_GetToken_ReturnUserDataObjectOnTrueData()
        {
            //Arrange
            MemberDetails details = new MemberDetails()
            {
                Id = 0,
                Name = "",
                Email = "sm123@gmail.com",
                Password = "hello",
                Location = ""
            };

            MemberDetails members = new MemberDetails{ Id = 1, Name = "Saurabh", Email = "sm123@gmail.com", Password = "hello", Location = "Haldwani" };
            //MockMember.Setup(x => x.Find(x => x.Email.Equals(details.Email) && x.Password.Equals(details.Password))).Returns(members);

            UserData user = new UserData{ Id = 1, Location = "Haldwani", Token = "TrileToken" };
            repository.Setup(x => x.GetToken(members)).Returns(user);

            //Act
            var result = provider.GetToken(details);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}