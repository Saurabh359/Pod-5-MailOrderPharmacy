using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RefillApi.Controllers;
using RefillApi.Models;
using RefillApi.Provider;
using RefillApi.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefillServiceTesting
{
    class ControllerTest
    {
        RefillOrder refillorder = new RefillOrder();
        List<RefillOrder> dataObject = new List<RefillOrder>();
        [SetUp]
        public void Setup()
        {
            dataObject = new List<RefillOrder>()
            {
            new RefillOrder{ Id =1, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=1},
            new RefillOrder{ Id =2, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=true,Payment=true, SubscriptionId=2},
            new RefillOrder{ Id =3, RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),DrugQuantity = 10, RefillDelivered=false,Payment=false, SubscriptionId=1}

            };
        }

        [TestCase(1)]
        public void ControllerTestPass1(int subscriptionId)
        {


            Mock<IRefillOrderProvider> mock = new Mock<IRefillOrderProvider>();
            mock.Setup(p => p.RefillStatus(subscriptionId)).Returns(refillorder);
            RefillOrdersController pc = new RefillOrdersController(mock.Object);
            var result = pc.RefillStatus(subscriptionId);
            Assert.IsNotNull(result);

        }


        [TestCase(-12)]
        public void ControllerTestFail1(int subscriptionId)
        {



            Mock<IRefillOrderProvider> mock = new Mock<IRefillOrderProvider>();
            mock.Setup(p => p.RefillStatus(subscriptionId)).Returns(refillorder);
            RefillOrdersController pc = new RefillOrdersController(mock.Object);
            var result = pc.RefillStatus(subscriptionId);
            Assert.IsNull(result);

        }



        [TestCase(1)]
        [TestCase(2)]
        public void ControllerTestPass2(int subscriptionId)
        {

            
            Mock<IRefillOrderProvider> mock = new Mock<IRefillOrderProvider>();
            mock.Setup(p => p.RefillDues(subscriptionId)).Returns(1);
            RefillOrdersController pc = new RefillOrdersController(mock.Object);
            var result = pc.RefillDues(subscriptionId) as OkObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestCase(-3)]
        [TestCase(-4)]
        [TestCase(-10)]
        public void ControllerTestFail2(int subscriptionId)
        {


            Mock<IRefillOrderProvider> mock = new Mock<IRefillOrderProvider>();
            mock.Setup(p => p.RefillDues(subscriptionId)).Returns(1);
            RefillOrdersController pc = new RefillOrdersController(mock.Object);
            var result =(BadRequestResult)pc.RefillDues(subscriptionId);
            Assert.AreEqual(400, result.StatusCode);

        }




    }
}
