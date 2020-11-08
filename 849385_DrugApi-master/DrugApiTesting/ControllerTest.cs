using Grpc.Core;
using MfpeDrugsApi.Controllers;
using MfpeDrugsApi.Models;
using MfpeDrugsApi.Provider;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugApiTesting
{
    [TestFixture]
    public class ControllerTest
    {
        private Mock<IProvider> config;
        private DrugsApiController TokenObj;
        [SetUp]
        public void Setup()
        {
            config = new Mock<IProvider>();
            TokenObj = new DrugsApiController(config.Object);
        }

        [Test]
        public void GetDetailsWhenNotNullDrugId()
        {
           config.Setup(p => p.searchDrugsByID(1)).Returns(new List<LocationWiseDrug>{new LocationWiseDrug()
            {

            } });
            var result = TokenObj.searchDrugsByID(1);
            var rs = result as OkObjectResult;
            var rs1 = rs.Value as List<LocationWiseDrug>;
            Assert.That(rs1, Is.Not.Empty);
          //  Assert.That(result,Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetDetailsWhenNullDrugId()
        {
          //  config.Setup(p => p.searchDrugsByID(0)).Returns(new List<LocationWiseDrug> { });
            var result = (BadRequestResult)TokenObj.searchDrugsByID(0);
            Assert.AreEqual(result.StatusCode,400);
        }
        [Test]
        public void GetDetailsWhenNotNullDrugName()
        {
            config.Setup(p => p.searchDrugsByName("Disprin")).Returns(new List<LocationWiseDrug>{new LocationWiseDrug()
            {

            } });
            var result = TokenObj.searchDrugsByName("Disprin");
            var rs = result as OkObjectResult;
            var rs1 = rs.Value as List<LocationWiseDrug>;
            Assert.That(rs1, Is.Not.Empty);
        }
        [Test]
        public void GetDetailsWhenNullDrugName()
        {
            var result = (BadRequestResult)TokenObj.searchDrugsByName(null);
            Assert.AreEqual(result.StatusCode, 400);
        }
        [Test]
        public void GetResponseWhenIdAndLocationIsNotNull()
        {
            //config.Setup(p => p.getDispatchableDrugStock(1, "Delhi")).Returns(() => true);
            var result = TokenObj.getDispatchableDrugStock(1, "Delhi");
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }
        [Test]
        public void GetResponseWhenIdAndLocationIsNull()
        {
            var result = (BadRequestResult)TokenObj.getDispatchableDrugStock(0,null);
            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
