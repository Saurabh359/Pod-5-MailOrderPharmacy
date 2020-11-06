using AuthorizationMicroService.Controllers;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.Providers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTestAuthorizationMicroService
{
    public class TokenControllerTesting
    {
        private TokenController controller;
        private Mock<ITokenProvider<MemberDetails>> provider;

        [SetUp]
        public void Setup()
        {
            provider = new Mock<ITokenProvider<MemberDetails>>();
            controller = new TokenController(provider.Object);
        }

        [TestCase("sm123@gmail.com", "hello")]
        [TestCase(null,null)]
        [TestCase("", null)]
        [TestCase(null, "hello")]
        [TestCase("sm123@gmail.com", "")]
        [TestCase("", "hello")]
        [TestCase("","")]
        public void Post_ReturnsBadRequest_OnNullorEmptyCredentials(string email,string password)
        {
            //Arrange
            MemberDetails details = new MemberDetails
            {
                Id = 0,
                Name = "",
                Email = "",
                Password = "",
                Location = ""
            };

            details.Email = email;
            details.Password = password;

            //Act
            var result = controller.Post(details);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Post_ReturnsBadRequest_OnNullDataObject()
        {
            //Arrange
            MemberDetails details = null;

            //Act
            var result = controller.Post(details);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Post_ReturnsSuccessObject_OnValidData()
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
            provider.Setup(x => x.GetToken(It.IsAny<MemberDetails>())).Returns(user);

            //Act
            var result = controller.Post(details);
            
            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
